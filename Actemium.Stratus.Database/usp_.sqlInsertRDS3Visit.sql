-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [StratusContext]
GO
-- =============================================
-- Author:		Mark Pitt
-- Create date: 27/03/14
-- Description:	Inserts a Visit from a set of DataTables (Table Valued Parammeters)
--				Created by parsing the RDS Result packet in application code 
-- =============================================

DROP PROCEDURE [dbo].[usp_InsertRDS3Visit]
GO
DROP TYPE [dbo].[ResultDescriptionTableType]
GO
DROP TYPE [dbo].[ResultTableType]
GO
DROP TYPE [dbo].[SequenceExecTableType]
GO
DROP TYPE [dbo].[SequenceTableType]
GO


CREATE TYPE SequenceExecTableType AS TABLE
(
	Id INT,
	VisitId INT,
	Sequence VARCHAR(MAX),
	StartTime DateTime,
	Duration INT,
	Status INT
);
GO

CREATE TYPE ResultTableType AS TABLE
(
	Id INT,
	SequenceExecutionId INT,
	RelativeTime FLOAT,
	ResultDescription VARCHAR(MAX),
	Type INT,
	Status INT,
	Value VARCHAR(MAX),
	LowerLimit VARCHAR(MAX),
	UpperLimit VARCHAR(MAX),
	Units VARCHAR(MAX),
	IsHidden BIT,
	IsFixed BIT,
	ParentResultId INT,
	DataType INT,
	ResultSource INT
);
GO

CREATE TYPE ResultDescriptionTableType AS TABLE
(
	Name VARCHAR(200)
);
GO

CREATE TYPE SequenceTableType AS TABLE
(
	Name VARCHAR(200)
);
GO

USE [StratusContext]
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertRDS3Visit]    Script Date: 02/04/2014 21:25:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_InsertRDS3Visit]
	@VisitStatus INT,
	@VisitStartTime DATETIME,
	@VisitEndTime DATETIME,
	@VisitDuration INT,
	@SeqExecs SequenceExecTableType READONLY,
	@Results ResultTableType READONLY,
	@Product VARCHAR(MAX),
	@Cell VARCHAR(MAX),
	@Company VARCHAR(MAX),
	@Location VARCHAR(MAX),
	@Plant VARCHAR(MAX),
	@Process VARCHAR(MAX),
	@ProductType VARCHAR(MAX),
	@ResultDescriptions ResultDescriptionTableType READONLY,
	@Sequences SequenceTableType READONLY,
	@Tester VARCHAR(MAX),
	@Zone VARCHAR(MAX),
	@VisitXml VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON 

	BEGIN TRAN

	DECLARE @ProductId INT
	DECLARE @CellId INT
	DECLARE @CompanyId INT
	DECLARE @LocationId INT
	DECLARE @PlantId INT
	DECLARE @ProcessId INT
	DECLARE @ProductTypeId INT
	DECLARE @SequenceId INT
	DECLARE @TesterId INT
	DECLARE @ZoneId INT
	DECLARE @VisitId INT

	SELECT @CellId = Id FROM Cells WHERE Cells.Name = @Cell
	IF @CellId IS NULL 
	BEGIN
		INSERT Cells(Name) VALUES (@Cell)
		SET @CellId = SCOPE_IDENTITY()
	END

	SELECT @CompanyId = Id FROM Companies WHERE Companies.Name = @Company
	IF @CompanyId IS NULL 
	BEGIN
		INSERT Companies(Name) VALUES (@Company)
		SET @CompanyId = SCOPE_IDENTITY()
	END

	SELECT @LocationId = Id FROM Locations WHERE Locations.Name = @Location
	IF @LocationId IS NULL 
	BEGIN
		INSERT Locations(Name) VALUES (@Location)
		SET @LocationId = SCOPE_IDENTITY()
	END

	SELECT @PlantId = Id FROM Plants WHERE Plants.Name = @Plant
	IF @PlantId IS NULL 
	BEGIN
		INSERT Plants(Name) VALUES (@Plant)
		SET @PlantId = SCOPE_IDENTITY()
	END

	SELECT @ProcessId = Id FROM Processes WHERE Processes.Name = @Process
	IF @ProcessId IS NULL
	BEGIN
		INSERT Processes(Name) VALUES (@Process)
		SET @ProcessId = SCOPE_IDENTITY()
	END

	SELECT @ProductTypeId = Id FROM ProductTypes WHERE ProductTypes.Name = @ProductType
	IF @ProductTypeId IS NULL
	BEGIN
		INSERT ProductTypes(Name) VALUES (@ProductType)
		SET @ProductTypeId = SCOPE_IDENTITY()
	END

	SELECT @TesterId = Id FROM Testers WHERE Testers.Name = @Tester
	IF @TesterId IS NULL
	BEGIN
		INSERT Testers(Name, TesterDescription) VALUES (@Tester, @Tester)
		SET @TesterId = SCOPE_IDENTITY()
	END

	SELECT @ZoneId = Id FROM Zones WHERE Zones.Name = @Zone
	IF @ZoneId IS NULL
	BEGIN
		INSERT Zones(Name) VALUES (@Zone)
		SET @ZoneId = SCOPE_IDENTITY()
	END

	SELECT @ProductId = Id FROM Products WHERE Products.ProductUniqueId = @Product
	IF @ProductId IS NULL 
	BEGIN
		INSERT Products(ProductUniqueId, ProductTypeId) VALUES (@Product, @ProductTypeId)
		SET @ProductId = SCOPE_IDENTITY()
	END

	--Add the Sequence names and result descriptions if they are not already included

	INSERT Sequences(Name) SELECT Name FROM @Sequences as S_TVP 
	WHERE NOT EXISTS(SELECT 1 FROM Sequences as S WHERE S_TVP.Name = S.Name)
	
	INSERT ResultDescriptions(Name) SELECT Name FROM @ResultDescriptions as RD_TVP 
	WHERE NOT EXISTS(SELECT 1 FROM ResultDescriptions as RD WHERE RD_TVP.Name = RD.Name)
	
	--Connect the many to many tables
	
	INSERT CellLocation(Cells_Id, Locations_Id) SELECT @CellId, @LocationId
	WHERE NOT EXISTS
	(SELECT 1 FROM CellLocation 
	WHERE CellLocation.Cells_Id = @CellId AND CellLocation.Locations_Id = @LocationId) 

	INSERT CompanyPlant(Companies_Id, Plants_Id) SELECT @CompanyId, @PlantId
	WHERE NOT EXISTS
	(SELECT 1 FROM CompanyPlant 
	WHERE CompanyPlant.Companies_Id = @CompanyId AND CompanyPlant.Plants_Id = @PlantId) 

	INSERT CompanyProduct(Companies_Id, Products_Id) SELECT @CompanyId, @ProductId
	WHERE NOT EXISTS
	(SELECT 1 FROM CompanyProduct 
	WHERE CompanyProduct.Companies_Id = @CompanyId AND CompanyProduct.Products_Id = @ProductId) 

	INSERT PlantTester(Plants_Id, Testers_Id) SELECT @PlantId, @TesterId
	WHERE NOT EXISTS
	(SELECT 1 FROM PlantTester 
	WHERE PlantTester.Plants_Id = @PlantId AND PlantTester.Testers_Id = @TesterId) 

	INSERT ProcessZone(Processes_Id, Zones_Id) SELECT @ProcessId, @ZoneId
	WHERE NOT EXISTS
	(SELECT 1 FROM ProcessZone 
	WHERE ProcessZone.Processes_Id = @ProcessId AND ProcessZone.Zones_Id = @ZoneId) 

	INSERT TesterProcess(Testers_Id, Processes_Id) SELECT @TesterId, @ProcessId
	WHERE NOT EXISTS
	(SELECT 1 FROM TesterProcess 
	WHERE TesterProcess.Testers_Id = @TesterId AND TesterProcess.Processes_Id = @ProcessId) 

	INSERT ZoneCell(Zones_Id, Cells_Id) SELECT @ZoneId, @CellId
	WHERE NOT EXISTS
	(SELECT 1 FROM ZoneCell 
	WHERE ZoneCell.Zones_Id = @ZoneId AND ZoneCell.Cells_Id = @CellId) 

	-- Add the visit
	INSERT INTO Visits
	(CellId, CompanyId, Duration, EndTime, LocationId, PlantId, ProcessId, ProductId, 
	StartTime, Status, TesterId, ZoneId, VisitXml) 
	VALUES
	(@CellId, @CompanyId, @VisitDuration, @VisitEndTime, @LocationId, @PlantId, @ProcessId,
	@ProductId, @VisitStartTime, @VisitStatus, @TesterId, @ZoneId, @VisitXml)
	SET @VisitId = SCOPE_IDENTITY()

	DECLARE @SeqExecRowCount INT
	SET @SeqExecRowCount = (SELECT COUNT(*) FROM @SeqExecs)

	DECLARE @SeqExecRow INT
	SET @SeqExecRow = 1

	-- Insert the Sequence Executions
	WHILE(@SeqExecRow <= @SeqExecRowCount)
	BEGIN
		DECLARE @Duration INT
		DECLARE @SequenceName VARCHAR(MAX)
		DECLARE @SequenceNameId INT
		DECLARE @StartTime DATETIME
		DECLARE @SeqStatus INT

		SELECT @Duration = Duration, 
			   @SequenceName = Sequence, 
			   @StartTime = StartTime, 
			   @SeqStatus = Status
		FROM @SeqExecs AS SE_TVP
		WHERE SE_TVP.Id = @SeqExecRow

		SELECT @SequenceNameId = Id 
		FROM Sequences 
		WHERE Sequences.Name = @SequenceName

		INSERT SequenceExecutions(Duration, SequenceId, StartTime, Status, VisitId)
		VALUES (@Duration, @SequenceNameId, @StartTime, @SeqStatus, @VisitId)
		
		DECLARE @CurrentSeqExecId INT
		SET @CurrentSeqExecId = SCOPE_IDENTITY()

		DECLARE @ResultsRowCount INT
		SET @ResultsRowCount = (SELECT COUNT(*) FROM @Results AS R_TVP)

		DECLARE @ResultRow INT
		SET @ResultRow = 1

		DECLARE @LastId INT

		WHILE(@ResultRow <= @ResultsRowCount)
		BEGIN
			
			DECLARE @ParentResultId INT
			DECLARE @NewParentId INT
			DECLARE @BaseResultId INT

			SELECT @ParentResultId = R_TVP.ParentResultId  FROM @Results AS R_TVP WHERE R_TVP.Id = @ResultRow AND R_TVP.SequenceExecutionId = @SeqExecRow

			IF(@ParentResultId IS NULL) 
				SET @NewParentId = NULL
			ELSE
				SET @NewParentId = @BaseResultId + @ParentResultId -1

			DECLARE	@IsHidden BIT
			DECLARE	@IsFixed BIT
			DECLARE	@LowerLimit VARCHAR(MAX)
			DECLARE	@RelativeTime FLOAT
			DECLARE	@ResultStatus INT
			DECLARE	@ResultType INT
			DECLARE	@Units VARCHAR(MAX)
			DECLARE	@UpperLimit VARCHAR(MAX)
			DECLARE	@ResultValue VARCHAR(MAX)
			DECLARE	@ResultDescription VARCHAR(MAX)
			DECLARE	@ResultDescriptionId INT
			DECLARE @DataType INT
			DECLARE @ResultSource INT
			
			IF EXISTS(SELECT 1 FROM @Results R_TVP WHERE R_TVP.Id = @ResultRow AND R_TVP.SequenceExecutionId = @SeqExecRow)
			BEGIN
				SELECT @IsHidden = R_TVP.IsHidden, 
					   @IsFixed = R_TVP.IsFixed, 
					   @LowerLimit = R_TVP.LowerLimit, 
					   @RelativeTime = R_TVP.RelativeTime,
					   @ResultStatus = R_TVP.Status, 
					   @ResultType = R_TVP.Type, 
					   @Units = R_TVP.Units, 
					   @UpperLimit = R_TVP.UpperLimit, 
					   @ResultValue = R_TVP.Value,
					   @ResultDescription = R_TVP.ResultDescription,
					   @DataType = R_TVP.DataType,
					   @ResultSource = R_TVP.ResultSource
				FROM @Results AS R_TVP
				WHERE R_TVP.Id = @ResultRow AND R_TVP.SequenceExecutionId = @SeqExecRow

				SELECT @ResultDescriptionId = Id FROM ResultDescriptions WHERE ResultDescriptions.Name = @ResultDescription

				MERGE INTO Results R
				USING (SELECT @ProductId AS ProductId, @ResultDescriptionId AS ResultDescriptionId, @SequenceNameId AS SequenceNameId) AS SRC
				ON (R.ProductId = SRC.ProductId 
				AND R.ResultDescriptionId = SRC.ResultDescriptionId 
				AND R.SequenceId = SRC.SequenceNameId
				AND R.Type != 8) --ParentType is 8
				WHEN MATCHED THEN
					UPDATE SET IsHistoric = 'TRUE';

				INSERT INTO Results(ResultDescriptionId, IsHidden, IsFixed, LowerLimit, RelativeTime, Status, Type, Units, UpperLimit, Value, 
									SequenceExecutionId, ParentResultId, IsHistoric, ProductId, SequenceId, DataType, ResultSource)
				VALUES (@ResultDescriptionId, @IsHidden, @IsFixed, @LowerLimit, @RelativeTime, @ResultStatus, @ResultType, @Units, @UpperLimit, @ResultValue, 
						@CurrentSeqExecId, @NewParentId, 'FALSE', @ProductId, @SequenceNameId, @DataType, @ResultSource)
			END

			IF(@ResultRow = 1)
				SET @BaseResultId = SCOPE_IDENTITY()
			
			SET @ResultRow = @ResultRow + 1
		END

		SET @SeqExecRow = @SeqExecRow + 1
	END

    SELECT @VisitId;

	COMMIT TRAN
END

GO
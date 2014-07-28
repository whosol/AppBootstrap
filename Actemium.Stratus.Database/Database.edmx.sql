
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/16/2014 12:38:26
-- Generated from EDMX file: C:\Users\mpitt.ACTEMIUM-FAS\Documents\Visual Studio 2013\Projects\Actemium.Stratus.ServiceController\Actemium.Stratus.Database\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [StratusContext];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_VisitSequenceExecution]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SequenceExecutions] DROP CONSTRAINT [FK_VisitSequenceExecution];
GO
IF OBJECT_ID(N'[dbo].[FK_SequenceSequenceExecution]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SequenceExecutions] DROP CONSTRAINT [FK_SequenceSequenceExecution];
GO
IF OBJECT_ID(N'[dbo].[FK_SequenceExecutionResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_SequenceExecutionResult];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_ProductVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductTypeProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ProductTypeProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_VisitTester]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_VisitTester];
GO
IF OBJECT_ID(N'[dbo].[FK_ZoneVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_ZoneVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_CellVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_CellVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_LocationVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_ProcessVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_PlantVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_PlantVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyVisit]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visits] DROP CONSTRAINT [FK_CompanyVisit];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultDescriptionResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_ResultDescriptionResult];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyProduct_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyProduct] DROP CONSTRAINT [FK_CompanyProduct_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyProduct_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyProduct] DROP CONSTRAINT [FK_CompanyProduct_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPlant_Company]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyPlant] DROP CONSTRAINT [FK_CompanyPlant_Company];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyPlant_Plant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyPlant] DROP CONSTRAINT [FK_CompanyPlant_Plant];
GO
IF OBJECT_ID(N'[dbo].[FK_PlantTester_Plant]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlantTester] DROP CONSTRAINT [FK_PlantTester_Plant];
GO
IF OBJECT_ID(N'[dbo].[FK_PlantTester_Tester]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlantTester] DROP CONSTRAINT [FK_PlantTester_Tester];
GO
IF OBJECT_ID(N'[dbo].[FK_TesterProcess_Tester]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TesterProcess] DROP CONSTRAINT [FK_TesterProcess_Tester];
GO
IF OBJECT_ID(N'[dbo].[FK_TesterProcess_Process]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TesterProcess] DROP CONSTRAINT [FK_TesterProcess_Process];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessZone_Process]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessZone] DROP CONSTRAINT [FK_ProcessZone_Process];
GO
IF OBJECT_ID(N'[dbo].[FK_ProcessZone_Zone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProcessZone] DROP CONSTRAINT [FK_ProcessZone_Zone];
GO
IF OBJECT_ID(N'[dbo].[FK_ZoneCell_Zone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZoneCell] DROP CONSTRAINT [FK_ZoneCell_Zone];
GO
IF OBJECT_ID(N'[dbo].[FK_ZoneCell_Cell]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ZoneCell] DROP CONSTRAINT [FK_ZoneCell_Cell];
GO
IF OBJECT_ID(N'[dbo].[FK_CellLocation_Cell]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CellLocation] DROP CONSTRAINT [FK_CellLocation_Cell];
GO
IF OBJECT_ID(N'[dbo].[FK_CellLocation_Location]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CellLocation] DROP CONSTRAINT [FK_CellLocation_Location];
GO
IF OBJECT_ID(N'[dbo].[FK_ResultResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_ResultResult];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_ProductResult];
GO
IF OBJECT_ID(N'[dbo].[FK_SequenceResult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Results] DROP CONSTRAINT [FK_SequenceResult];
GO
IF OBJECT_ID(N'[dbo].[FK_UserClaim_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClaims] DROP CONSTRAINT [FK_UserClaim_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserLogin_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT [FK_UserLogin_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserRole_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUserRole] DROP CONSTRAINT [FK_UserUserRole_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserRole_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserUserRole] DROP CONSTRAINT [FK_UserUserRole_UserRole];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Plants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plants];
GO
IF OBJECT_ID(N'[dbo].[Zones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zones];
GO
IF OBJECT_ID(N'[dbo].[Cells]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cells];
GO
IF OBJECT_ID(N'[dbo].[ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[Visits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visits];
GO
IF OBJECT_ID(N'[dbo].[SequenceExecutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SequenceExecutions];
GO
IF OBJECT_ID(N'[dbo].[Sequences]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sequences];
GO
IF OBJECT_ID(N'[dbo].[Results]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Results];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[Testers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Testers];
GO
IF OBJECT_ID(N'[dbo].[Processes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Processes];
GO
IF OBJECT_ID(N'[dbo].[ResultDescriptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResultDescriptions];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[UserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserClaims];
GO
IF OBJECT_ID(N'[dbo].[UserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogins];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[CompanyProduct]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyProduct];
GO
IF OBJECT_ID(N'[dbo].[CompanyPlant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyPlant];
GO
IF OBJECT_ID(N'[dbo].[PlantTester]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlantTester];
GO
IF OBJECT_ID(N'[dbo].[TesterProcess]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TesterProcess];
GO
IF OBJECT_ID(N'[dbo].[ProcessZone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProcessZone];
GO
IF OBJECT_ID(N'[dbo].[ZoneCell]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZoneCell];
GO
IF OBJECT_ID(N'[dbo].[CellLocation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CellLocation];
GO
IF OBJECT_ID(N'[dbo].[UserUserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserUserRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProductUniqueId] nvarchar(max)  NOT NULL,
    [ProductTypeId] int  NOT NULL
);
GO

-- Creating table 'Plants'
CREATE TABLE [dbo].[Plants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Zones'
CREATE TABLE [dbo].[Zones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cells'
CREATE TABLE [dbo].[Cells] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Visits'
CREATE TABLE [dbo].[Visits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [Duration] int  NOT NULL,
    [ProductId] int  NOT NULL,
    [TesterId] int  NOT NULL,
    [ZoneId] int  NOT NULL,
    [CellId] int  NOT NULL,
    [LocationId] int  NOT NULL,
    [ProcessId] int  NOT NULL,
    [PlantId] int  NOT NULL,
    [CompanyId] int  NOT NULL,
    [VisitXml] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SequenceExecutions'
CREATE TABLE [dbo].[SequenceExecutions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VisitId] int  NOT NULL,
    [SequenceId] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Duration] int  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'Sequences'
CREATE TABLE [dbo].[Sequences] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Results'
CREATE TABLE [dbo].[Results] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SequenceExecutionId] int  NOT NULL,
    [RelativeTime] float  NOT NULL,
    [ResultDescriptionId] int  NOT NULL,
    [Type] int  NOT NULL,
    [Status] int  NULL,
    [Value] nvarchar(max)  NULL,
    [LowerLimit] nvarchar(max)  NULL,
    [UpperLimit] nvarchar(max)  NULL,
    [Units] nvarchar(max)  NULL,
    [IsHidden] bit  NULL,
    [IsFixed] bit  NULL,
    [ParentResultId] int  NULL,
    [IsHistoric] bit  NOT NULL,
    [ProductId] int  NOT NULL,
    [SequenceId] int  NOT NULL,
    [DataType] int  NULL,
    [ResultSource] int  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Testers'
CREATE TABLE [dbo].[Testers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TesterDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Processes'
CREATE TABLE [dbo].[Processes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ResultDescriptions'
CREATE TABLE [dbo].[ResultDescriptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(100)  NULL,
    [SecurityStamp] nvarchar(100)  NULL,
    [PhoneNumber] nvarchar(25)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL
);
GO

-- Creating table 'UserClaims'
CREATE TABLE [dbo].[UserClaims] (
    [UserId] int  NOT NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'UserLogins'
CREATE TABLE [dbo].[UserLogins] (
    [UserId] int  NOT NULL,
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Id] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'CompanyProduct'
CREATE TABLE [dbo].[CompanyProduct] (
    [Companies_Id] int  NOT NULL,
    [Products_Id] int  NOT NULL
);
GO

-- Creating table 'CompanyPlant'
CREATE TABLE [dbo].[CompanyPlant] (
    [Companies_Id] int  NOT NULL,
    [Plants_Id] int  NOT NULL
);
GO

-- Creating table 'PlantTester'
CREATE TABLE [dbo].[PlantTester] (
    [Plants_Id] int  NOT NULL,
    [Testers_Id] int  NOT NULL
);
GO

-- Creating table 'TesterProcess'
CREATE TABLE [dbo].[TesterProcess] (
    [Testers_Id] int  NOT NULL,
    [Processes_Id] int  NOT NULL
);
GO

-- Creating table 'ProcessZone'
CREATE TABLE [dbo].[ProcessZone] (
    [Processes_Id] int  NOT NULL,
    [Zones_Id] int  NOT NULL
);
GO

-- Creating table 'ZoneCell'
CREATE TABLE [dbo].[ZoneCell] (
    [Zones_Id] int  NOT NULL,
    [Cells_Id] int  NOT NULL
);
GO

-- Creating table 'CellLocation'
CREATE TABLE [dbo].[CellLocation] (
    [Cells_Id] int  NOT NULL,
    [Locations_Id] int  NOT NULL
);
GO

-- Creating table 'UserUserRole'
CREATE TABLE [dbo].[UserUserRole] (
    [Users_Id] int  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Plants'
ALTER TABLE [dbo].[Plants]
ADD CONSTRAINT [PK_Plants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Zones'
ALTER TABLE [dbo].[Zones]
ADD CONSTRAINT [PK_Zones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cells'
ALTER TABLE [dbo].[Cells]
ADD CONSTRAINT [PK_Cells]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [PK_Visits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SequenceExecutions'
ALTER TABLE [dbo].[SequenceExecutions]
ADD CONSTRAINT [PK_SequenceExecutions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sequences'
ALTER TABLE [dbo].[Sequences]
ADD CONSTRAINT [PK_Sequences]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [PK_Results]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Testers'
ALTER TABLE [dbo].[Testers]
ADD CONSTRAINT [PK_Testers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Processes'
ALTER TABLE [dbo].[Processes]
ADD CONSTRAINT [PK_Processes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ResultDescriptions'
ALTER TABLE [dbo].[ResultDescriptions]
ADD CONSTRAINT [PK_ResultDescriptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [PK_UserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [LoginProvider], [ProviderKey] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [PK_UserLogins]
    PRIMARY KEY CLUSTERED ([UserId], [LoginProvider], [ProviderKey] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Companies_Id], [Products_Id] in table 'CompanyProduct'
ALTER TABLE [dbo].[CompanyProduct]
ADD CONSTRAINT [PK_CompanyProduct]
    PRIMARY KEY CLUSTERED ([Companies_Id], [Products_Id] ASC);
GO

-- Creating primary key on [Companies_Id], [Plants_Id] in table 'CompanyPlant'
ALTER TABLE [dbo].[CompanyPlant]
ADD CONSTRAINT [PK_CompanyPlant]
    PRIMARY KEY CLUSTERED ([Companies_Id], [Plants_Id] ASC);
GO

-- Creating primary key on [Plants_Id], [Testers_Id] in table 'PlantTester'
ALTER TABLE [dbo].[PlantTester]
ADD CONSTRAINT [PK_PlantTester]
    PRIMARY KEY CLUSTERED ([Plants_Id], [Testers_Id] ASC);
GO

-- Creating primary key on [Testers_Id], [Processes_Id] in table 'TesterProcess'
ALTER TABLE [dbo].[TesterProcess]
ADD CONSTRAINT [PK_TesterProcess]
    PRIMARY KEY CLUSTERED ([Testers_Id], [Processes_Id] ASC);
GO

-- Creating primary key on [Processes_Id], [Zones_Id] in table 'ProcessZone'
ALTER TABLE [dbo].[ProcessZone]
ADD CONSTRAINT [PK_ProcessZone]
    PRIMARY KEY CLUSTERED ([Processes_Id], [Zones_Id] ASC);
GO

-- Creating primary key on [Zones_Id], [Cells_Id] in table 'ZoneCell'
ALTER TABLE [dbo].[ZoneCell]
ADD CONSTRAINT [PK_ZoneCell]
    PRIMARY KEY CLUSTERED ([Zones_Id], [Cells_Id] ASC);
GO

-- Creating primary key on [Cells_Id], [Locations_Id] in table 'CellLocation'
ALTER TABLE [dbo].[CellLocation]
ADD CONSTRAINT [PK_CellLocation]
    PRIMARY KEY CLUSTERED ([Cells_Id], [Locations_Id] ASC);
GO

-- Creating primary key on [Users_Id], [Roles_Id] in table 'UserUserRole'
ALTER TABLE [dbo].[UserUserRole]
ADD CONSTRAINT [PK_UserUserRole]
    PRIMARY KEY CLUSTERED ([Users_Id], [Roles_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [VisitId] in table 'SequenceExecutions'
ALTER TABLE [dbo].[SequenceExecutions]
ADD CONSTRAINT [FK_VisitSequenceExecution]
    FOREIGN KEY ([VisitId])
    REFERENCES [dbo].[Visits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitSequenceExecution'
CREATE INDEX [IX_FK_VisitSequenceExecution]
ON [dbo].[SequenceExecutions]
    ([VisitId]);
GO

-- Creating foreign key on [SequenceId] in table 'SequenceExecutions'
ALTER TABLE [dbo].[SequenceExecutions]
ADD CONSTRAINT [FK_SequenceSequenceExecution]
    FOREIGN KEY ([SequenceId])
    REFERENCES [dbo].[Sequences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SequenceSequenceExecution'
CREATE INDEX [IX_FK_SequenceSequenceExecution]
ON [dbo].[SequenceExecutions]
    ([SequenceId]);
GO

-- Creating foreign key on [SequenceExecutionId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_SequenceExecutionResult]
    FOREIGN KEY ([SequenceExecutionId])
    REFERENCES [dbo].[SequenceExecutions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SequenceExecutionResult'
CREATE INDEX [IX_FK_SequenceExecutionResult]
ON [dbo].[Results]
    ([SequenceExecutionId]);
GO

-- Creating foreign key on [ProductId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_ProductVisit]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductVisit'
CREATE INDEX [IX_FK_ProductVisit]
ON [dbo].[Visits]
    ([ProductId]);
GO

-- Creating foreign key on [ProductTypeId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ProductTypeProduct]
    FOREIGN KEY ([ProductTypeId])
    REFERENCES [dbo].[ProductTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductTypeProduct'
CREATE INDEX [IX_FK_ProductTypeProduct]
ON [dbo].[Products]
    ([ProductTypeId]);
GO

-- Creating foreign key on [TesterId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_VisitTester]
    FOREIGN KEY ([TesterId])
    REFERENCES [dbo].[Testers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VisitTester'
CREATE INDEX [IX_FK_VisitTester]
ON [dbo].[Visits]
    ([TesterId]);
GO

-- Creating foreign key on [ZoneId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_ZoneVisit]
    FOREIGN KEY ([ZoneId])
    REFERENCES [dbo].[Zones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZoneVisit'
CREATE INDEX [IX_FK_ZoneVisit]
ON [dbo].[Visits]
    ([ZoneId]);
GO

-- Creating foreign key on [CellId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_CellVisit]
    FOREIGN KEY ([CellId])
    REFERENCES [dbo].[Cells]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CellVisit'
CREATE INDEX [IX_FK_CellVisit]
ON [dbo].[Visits]
    ([CellId]);
GO

-- Creating foreign key on [LocationId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_LocationVisit]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationVisit'
CREATE INDEX [IX_FK_LocationVisit]
ON [dbo].[Visits]
    ([LocationId]);
GO

-- Creating foreign key on [ProcessId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_ProcessVisit]
    FOREIGN KEY ([ProcessId])
    REFERENCES [dbo].[Processes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessVisit'
CREATE INDEX [IX_FK_ProcessVisit]
ON [dbo].[Visits]
    ([ProcessId]);
GO

-- Creating foreign key on [PlantId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_PlantVisit]
    FOREIGN KEY ([PlantId])
    REFERENCES [dbo].[Plants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlantVisit'
CREATE INDEX [IX_FK_PlantVisit]
ON [dbo].[Visits]
    ([PlantId]);
GO

-- Creating foreign key on [CompanyId] in table 'Visits'
ALTER TABLE [dbo].[Visits]
ADD CONSTRAINT [FK_CompanyVisit]
    FOREIGN KEY ([CompanyId])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyVisit'
CREATE INDEX [IX_FK_CompanyVisit]
ON [dbo].[Visits]
    ([CompanyId]);
GO

-- Creating foreign key on [ResultDescriptionId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_ResultDescriptionResult]
    FOREIGN KEY ([ResultDescriptionId])
    REFERENCES [dbo].[ResultDescriptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultDescriptionResult'
CREATE INDEX [IX_FK_ResultDescriptionResult]
ON [dbo].[Results]
    ([ResultDescriptionId]);
GO

-- Creating foreign key on [Companies_Id] in table 'CompanyProduct'
ALTER TABLE [dbo].[CompanyProduct]
ADD CONSTRAINT [FK_CompanyProduct_Company]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Products_Id] in table 'CompanyProduct'
ALTER TABLE [dbo].[CompanyProduct]
ADD CONSTRAINT [FK_CompanyProduct_Product]
    FOREIGN KEY ([Products_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyProduct_Product'
CREATE INDEX [IX_FK_CompanyProduct_Product]
ON [dbo].[CompanyProduct]
    ([Products_Id]);
GO

-- Creating foreign key on [Companies_Id] in table 'CompanyPlant'
ALTER TABLE [dbo].[CompanyPlant]
ADD CONSTRAINT [FK_CompanyPlant_Company]
    FOREIGN KEY ([Companies_Id])
    REFERENCES [dbo].[Companies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Plants_Id] in table 'CompanyPlant'
ALTER TABLE [dbo].[CompanyPlant]
ADD CONSTRAINT [FK_CompanyPlant_Plant]
    FOREIGN KEY ([Plants_Id])
    REFERENCES [dbo].[Plants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyPlant_Plant'
CREATE INDEX [IX_FK_CompanyPlant_Plant]
ON [dbo].[CompanyPlant]
    ([Plants_Id]);
GO

-- Creating foreign key on [Plants_Id] in table 'PlantTester'
ALTER TABLE [dbo].[PlantTester]
ADD CONSTRAINT [FK_PlantTester_Plant]
    FOREIGN KEY ([Plants_Id])
    REFERENCES [dbo].[Plants]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Testers_Id] in table 'PlantTester'
ALTER TABLE [dbo].[PlantTester]
ADD CONSTRAINT [FK_PlantTester_Tester]
    FOREIGN KEY ([Testers_Id])
    REFERENCES [dbo].[Testers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlantTester_Tester'
CREATE INDEX [IX_FK_PlantTester_Tester]
ON [dbo].[PlantTester]
    ([Testers_Id]);
GO

-- Creating foreign key on [Testers_Id] in table 'TesterProcess'
ALTER TABLE [dbo].[TesterProcess]
ADD CONSTRAINT [FK_TesterProcess_Tester]
    FOREIGN KEY ([Testers_Id])
    REFERENCES [dbo].[Testers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Processes_Id] in table 'TesterProcess'
ALTER TABLE [dbo].[TesterProcess]
ADD CONSTRAINT [FK_TesterProcess_Process]
    FOREIGN KEY ([Processes_Id])
    REFERENCES [dbo].[Processes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TesterProcess_Process'
CREATE INDEX [IX_FK_TesterProcess_Process]
ON [dbo].[TesterProcess]
    ([Processes_Id]);
GO

-- Creating foreign key on [Processes_Id] in table 'ProcessZone'
ALTER TABLE [dbo].[ProcessZone]
ADD CONSTRAINT [FK_ProcessZone_Process]
    FOREIGN KEY ([Processes_Id])
    REFERENCES [dbo].[Processes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Zones_Id] in table 'ProcessZone'
ALTER TABLE [dbo].[ProcessZone]
ADD CONSTRAINT [FK_ProcessZone_Zone]
    FOREIGN KEY ([Zones_Id])
    REFERENCES [dbo].[Zones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProcessZone_Zone'
CREATE INDEX [IX_FK_ProcessZone_Zone]
ON [dbo].[ProcessZone]
    ([Zones_Id]);
GO

-- Creating foreign key on [Zones_Id] in table 'ZoneCell'
ALTER TABLE [dbo].[ZoneCell]
ADD CONSTRAINT [FK_ZoneCell_Zone]
    FOREIGN KEY ([Zones_Id])
    REFERENCES [dbo].[Zones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Cells_Id] in table 'ZoneCell'
ALTER TABLE [dbo].[ZoneCell]
ADD CONSTRAINT [FK_ZoneCell_Cell]
    FOREIGN KEY ([Cells_Id])
    REFERENCES [dbo].[Cells]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZoneCell_Cell'
CREATE INDEX [IX_FK_ZoneCell_Cell]
ON [dbo].[ZoneCell]
    ([Cells_Id]);
GO

-- Creating foreign key on [Cells_Id] in table 'CellLocation'
ALTER TABLE [dbo].[CellLocation]
ADD CONSTRAINT [FK_CellLocation_Cell]
    FOREIGN KEY ([Cells_Id])
    REFERENCES [dbo].[Cells]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Locations_Id] in table 'CellLocation'
ALTER TABLE [dbo].[CellLocation]
ADD CONSTRAINT [FK_CellLocation_Location]
    FOREIGN KEY ([Locations_Id])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CellLocation_Location'
CREATE INDEX [IX_FK_CellLocation_Location]
ON [dbo].[CellLocation]
    ([Locations_Id]);
GO

-- Creating foreign key on [ParentResultId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_ResultResult]
    FOREIGN KEY ([ParentResultId])
    REFERENCES [dbo].[Results]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResultResult'
CREATE INDEX [IX_FK_ResultResult]
ON [dbo].[Results]
    ([ParentResultId]);
GO

-- Creating foreign key on [ProductId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_ProductResult]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductResult'
CREATE INDEX [IX_FK_ProductResult]
ON [dbo].[Results]
    ([ProductId]);
GO

-- Creating foreign key on [SequenceId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_SequenceResult]
    FOREIGN KEY ([SequenceId])
    REFERENCES [dbo].[Sequences]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SequenceResult'
CREATE INDEX [IX_FK_SequenceResult]
ON [dbo].[Results]
    ([SequenceId]);
GO

-- Creating foreign key on [UserId] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [FK_UserClaim_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserClaim_User'
CREATE INDEX [IX_FK_UserClaim_User]
ON [dbo].[UserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [FK_UserLogin_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'UserUserRole'
ALTER TABLE [dbo].[UserUserRole]
ADD CONSTRAINT [FK_UserUserRole_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_Id] in table 'UserUserRole'
ALTER TABLE [dbo].[UserUserRole]
ADD CONSTRAINT [FK_UserUserRole_UserRole]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[UserRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserRole_UserRole'
CREATE INDEX [IX_FK_UserUserRole_UserRole]
ON [dbo].[UserUserRole]
    ([Roles_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
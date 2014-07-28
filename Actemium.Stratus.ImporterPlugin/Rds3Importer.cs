using WhoSol.Contracts;
using WhoSol.Contracts.Enums;
using Actemium.Stratus.DataObjects;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.ImporterPlugin
{
    public sealed class Rds3Importer : IImporter, IDisposable
    {
        #region Fields

        private readonly IStratusUnitOfWork uow;
        private readonly ILogger logger;
        private DataTable resultTable;
        private DataTable resultDescriptionTable;
        private DataTable sequenceTable;
        private DataTable sequenceExecutionsTable;
        private string companyName;
        private string plantName;
        private string testerName;
        private string processName;
        private string zoneName;
        private string cellName;
        private string locationName;
        private string productUniqueId;
        private string productTypeName;
        private int visitStatus;
        private DateTime visitStartTime;
        private DateTime visitEndTime;
        private int visitDuration;
        private int resId;
        private string visitXmlData;

        #endregion

        public Rds3Importer(IStratusUnitOfWork uow, ILogger logger)
        {
            this.uow = uow;
            this.logger = logger;
        }


        #region Public

        public PersistStatus PersistResult(XDocument visitXml)
        {
            try
            {
                visitStartTime = (DateTime)visitXml.Element("VEHICLE").Element("VISIT").Attribute("start");
                visitEndTime = (DateTime)visitXml.Element("VEHICLE").Element("VISIT").Attribute("end");
                visitDuration = (int)visitXml.Element("VEHICLE").Element("VISIT").Attribute("dur");
                productUniqueId = (string)visitXml.Element("VEHICLE").Attribute("vin");
                visitXmlData = visitXml.ToString();

                if (uow.Visits.FindBy(v => v.StartTime == visitStartTime &&
                    v.EndTime == visitEndTime &&
                    v.Duration == visitDuration &&
                    v.Product.ProductUniqueId == productUniqueId).SingleOrDefault() == null)
                {
                    CreateTables();
                    companyName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("co");
                    plantName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("pl");
                    testerName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("tester");
                    processName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("pf");
                    zoneName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("zo");
                    cellName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("cell");
                    locationName = (string)visitXml.Element("VEHICLE").Element("VISIT").Attribute("lo");
                    productTypeName = (string)visitXml.Element("VEHICLE").Attribute("model");
                    visitStatus = GetVisitStatus(visitXml.Element("VEHICLE").Element("VISIT").Attribute("flags").Value.Split(','));

                    AddSequenceExecutions(visitXml.Element("VEHICLE").Element("VISIT"));

                    CallSP();
                }
                else
                {
                    return PersistStatus.Duplicate;
                }
            }
            catch (XmlException)
            {
                return PersistStatus.InvalidFormat;
            }
            catch(NullReferenceException)
            {
                return PersistStatus.InvalidFormat;
            }
            catch(Exception)
            {
                return PersistStatus.DatabaseError;
            }

            return PersistStatus.OK;
        }

        #endregion

        #region Private XML Parse Helpers

        private void AddSequenceExecutions(XElement xmlElement)
        {
            var seqExId = 1;
            foreach (var sequenceElement in xmlElement.Elements("SEQUENCE"))
            {
                var sequenceName = sequenceElement.Attribute("name").Value;
                if (!sequenceTable.Rows.Contains(sequenceName))
                {
                    sequenceTable.Rows.Add(sequenceName);
                }

                var startTime = (DateTime)sequenceElement.Attribute("start");
                var duration = (int)sequenceElement.Attribute("dur");

                if (uow.SequenceExecutions.FindBy(se => (se.Sequence.Name == sequenceName) &&
                    (se.StartTime == startTime) &&
                    (se.Duration == duration) &&
                    (se.Visit.Product.ProductUniqueId == productUniqueId)).FirstOrDefault() == null)
                {
                    var status = GetSequenceStatus(xmlElement.Attribute("flags").Value.Split(','));

                    sequenceExecutionsTable.Rows.Add(seqExId, 1, sequenceName, startTime, duration, status);
                    resId = 0;
                    AddResults(sequenceElement.Element("RESULTS").Elements(), seqExId);
                }
                seqExId++;
            }
        }

        private void AddResults(IEnumerable<XElement> elements, int sequenceExecutionId, int? parentResultId = null)
        {
            foreach (var resultElement in elements)
            {
                resId++;

                string[] values = null;

                var resultDescriptionName = (string)resultElement.Attribute("name");
                if (resultDescriptionTable.Rows.Find(resultDescriptionName) == null)
                {
                    resultDescriptionTable.Rows.Add(resultDescriptionName);
                }

                var resultSource = GetResultSource(resultElement.Name.LocalName);

                switch (resultElement.Name.LocalName)
                {
                    case "VB":
                    case "VPB":
                    case "VPPB":
                    case "VPPPB":
                    case "RB":
                    case "IB":
                        AddResult(resId, resultDescriptionName, (int)ResultType.Boolean, sequenceExecutionId,
                            resultStatus: (int)(resultElement.Value == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VO":
                    case "VPO":
                    case "VPPO":
                    case "VPPPO":
                    case "RO":
                    case "IO":
                        AddResult(resId, resultDescriptionName, (int)ResultType.Operator, sequenceExecutionId,
                            resultStatus: (int)(resultElement.Value == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VN":
                    case "VPN":
                    case "VPPN":
                    case "VPPPN":
                    case "RN":
                    case "IN":
                        values = GetValues(resultElement, values, 2, ',');
                        AddResult(resId, resultDescriptionName, (int)ResultType.Numeric, sequenceExecutionId,
                            resultStatus: (int)(values[0] == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            value: values[1],
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VG":
                    case "VPG":
                    case "VPPG":
                    case "VPPPG":
                    case "RG":
                    case "IG":
                        AddResult(resId, resultDescriptionName, (int)ResultType.Grouped, sequenceExecutionId,
                            resultStatus: (int)(resultElement.Value == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VP":
                    case "VPP":
                    case "VPPP":
                        AddResult(resId, resultDescriptionName, (int)ResultType.Parent, sequenceExecutionId,
                            resultStatus: (int)(resultElement.Value == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        AddResults(resultElement.Elements(), sequenceExecutionId, resId);
                        break;
                    case "VF":
                    case "VPF":
                    case "VPPF":
                    case "VPPPF":
                    case "VV":
                    case "VPV":
                    case "VPPV":
                    case "VPPPV":
                    case "RF":
                    case "RV":
                    case "IF":
                    case "IV":
                        values = GetValues(resultElement, values, 5, ',');
                        AddResult(resId, resultDescriptionName, (int)ResultType.Bounded, sequenceExecutionId,
                            resultStatus: (int)(values[0] == "P" ? ResultStatus.Pass : ResultStatus.Fail),
                            value: values[1],
                            lowerLimit: values[2],
                            upperLimit: values[3],
                            units: values[4],
                            isFixed: resultElement.Name.LocalName.EndsWith("F") ? true : false,
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VS":
                    case "VPS":
                    case "VPPS":
                    case "VPPPS":
                    case "VH":
                    case "VPH":
                    case "VPPH":
                    case "VPPPH":
                    case "RS":
                    case "IS":
                        AddResult(resId, resultDescriptionName, (int)ResultType.String, sequenceExecutionId,
                            value: (string)resultElement,
                            isHidden: resultElement.Name.LocalName.EndsWith("H") ? true : false,
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VA":
                    case "VPA":
                    case "VPPA":
                    case "VPPPA":
                    case "RA":
                    case "IA":
                        AddResult(resId, resultDescriptionName, (int)ResultType.Array, sequenceExecutionId,
                            value: (string)resultElement,
                            parentResultId: parentResultId,
                            resultSource: (int)resultSource);
                        break;
                    case "VD":
                    case "VPD":
                    case "VPPD":
                    case "VPPPD":
                    case "RD":
                    case "ID":
                        AddResult(resId, resultDescriptionName, (int)ResultType.DataFile, sequenceExecutionId,
                            value: (string)resultElement,
                            parentResultId: parentResultId,
                            dataFileType: (int)Enum.Parse(typeof(DataFileType), (string)resultElement.Attribute("type")),
                            resultSource: (int)resultSource);
                        break;
                    default:
                        break;
                }
            }
        }

        private ResultSource GetResultSource(string elementName)
        {
            switch (elementName.Substring(0, 1))
            {
                case "V":
                    return ResultSource.Product;
                case "R":
                    return ResultSource.Rig;
                case "I":
                    return ResultSource.Information;
                default:
                    throw new ApplicationException("Unknown result source type");
            }
        }

        private string[] GetValues(XElement resultElement, string[] values, int expected, char seperator)
        {
            values = ((string)resultElement).Split(seperator);
            if (values.Length != expected)
            {
                throw new FormatException("The result is in the wrong format");
            }
            return values;
        }

        private int GetVisitStatus(string[] flags)
        {
            return (int)((flags.Contains("P") ? VisitStatus.Pass : VisitStatus.None) |
                (flags.Contains("F") ? VisitStatus.Fail : VisitStatus.None) |
                (flags.Contains("A") ? VisitStatus.Aborted : VisitStatus.None));
        }

        private int GetSequenceStatus(string[] flags)
        {
            return (int)((flags.Contains("P") ? SequenceStatus.Pass : SequenceStatus.None) |
                        (flags.Contains("F") ? SequenceStatus.Fail : SequenceStatus.None) |
                        (flags.Contains("A") ? SequenceStatus.Aborted : SequenceStatus.None) |
                        (flags.Contains("R") ? SequenceStatus.Released : SequenceStatus.None));
        }

        #endregion

        #region Private DataTable Helpers

        private void CreateTables()
        {
            resultDescriptionTable = CreateNamedTable("ResultDescriptions");
            sequenceTable = CreateNamedTable("Sequences");
            resultTable = CreateResultTable();
            sequenceExecutionsTable = CreateSequenceExecutionTable();
        }

        private DataTable CreateResultTable()
        {
            var resultTable = new DataTable("Results");
            resultTable.Columns.Add("Id", typeof(int));
            resultTable.Columns.Add("SequenceExecutionId", typeof(int));
            resultTable.Columns.Add("RelativeTime", typeof(double));
            resultTable.Columns.Add("ResultDescription", typeof(string));
            resultTable.Columns.Add("Type", typeof(int));
            var statusColumn = resultTable.Columns.Add("Status", typeof(int));
            statusColumn.AllowDBNull = true;
            resultTable.Columns.Add("Value", typeof(string));
            resultTable.Columns.Add("LowerLimit", typeof(string));
            resultTable.Columns.Add("UpperLimit", typeof(string));
            resultTable.Columns.Add("Units", typeof(string));
            var isHiddenColumn = resultTable.Columns.Add("IsHidden", typeof(bool));
            isHiddenColumn.AllowDBNull = true;
            var isFixedColumn = resultTable.Columns.Add("IsFixed", typeof(bool));
            isFixedColumn.AllowDBNull = true;
            var parentResultIdColumn = resultTable.Columns.Add("ParentResultId", typeof(int));
            parentResultIdColumn.AllowDBNull = true;
            var dataTypeColumn = resultTable.Columns.Add("DataType", typeof(int));
            dataTypeColumn.AllowDBNull = true;
            resultTable.Columns.Add("ResultSource", typeof(int));

            return resultTable;
        }

        private DataTable CreateSequenceExecutionTable()
        {
            var sequenceExecutionTable = new DataTable("SequenceExecutions");
            sequenceExecutionTable.Columns.Add("Id", typeof(int));
            sequenceExecutionTable.Columns.Add("VisitId", typeof(int));
            sequenceExecutionTable.Columns.Add("Sequence", typeof(string));
            sequenceExecutionTable.Columns.Add("StartTime", typeof(DateTime));
            sequenceExecutionTable.Columns.Add("Duration", typeof(int));
            sequenceExecutionTable.Columns.Add("Status", typeof(int));
            return sequenceExecutionTable;
        }

        private DataTable CreateNamedTable(string tableName)
        {
            var namedTable = new DataTable(tableName);
            var keyColumn = namedTable.Columns.Add("Name");
            keyColumn.Unique = true;
            namedTable.PrimaryKey = new[] { keyColumn };
            return namedTable;
        }

        private void AddResult(int id, string resultDescription, int resultType,
            int sequenceExecutionId, int resultStatus = 0, int? parentResultId = null,
            int relativeTime = 0, string value = null, string lowerLimit = null,
            string upperLimit = null, string units = null, bool? isHidden = null,
            bool? isFixed = null, int? dataFileType = null, int resultSource = (int)ResultSource.Product)
        {
            resultTable.Rows.Add(id, sequenceExecutionId, relativeTime, resultDescription, resultType, resultStatus,
                value, lowerLimit, upperLimit, units, isHidden, isFixed, parentResultId, dataFileType, resultSource);
        }

        private void CallSP()
        {
            var visitStatusParam = new SqlParameter("@VisitStatus", visitStatus);
            var visitStartTimeParam = new SqlParameter("@VisitStartTime", visitStartTime);
            var visitEndTimeParam = new SqlParameter("@VisitEndTime", visitEndTime);
            var visitDurationParam = new SqlParameter("@VisitDuration", visitDuration);

            var productParam = new SqlParameter("@Product", productUniqueId);
            var cellParam = new SqlParameter("@Cell", cellName);
            var companyParam = new SqlParameter("@Company", companyName);
            var locationParam = new SqlParameter("@Location", locationName);
            var plantParam = new SqlParameter("@Plant", plantName);
            var processParam = new SqlParameter("@Process", processName);
            var productTypeParam = new SqlParameter("@ProductType", productTypeName);
            var testerParam = new SqlParameter("@Tester", testerName);
            var zoneParam = new SqlParameter("@Zone", zoneName);
            var visitXmlParam = new SqlParameter("@VisitXml", visitXmlData);

            var seqExecParam = new SqlParameter("@SeqExecs", SqlDbType.Structured);
            seqExecParam.TypeName = "SequenceExecTableType";
            seqExecParam.Value = sequenceExecutionsTable;

            var resultParam = new SqlParameter("@Results", SqlDbType.Structured);
            resultParam.TypeName = "ResultTableType";
            resultParam.Value = resultTable;

            var resultDescriptionParam = new SqlParameter("@ResultDescriptions", SqlDbType.Structured);
            resultDescriptionParam.TypeName = "ResultDescriptionTableType";
            resultDescriptionParam.Value = resultDescriptionTable;

            var sequenceParam = new SqlParameter("@Sequences", SqlDbType.Structured);
            sequenceParam.TypeName = "SequenceTableType";
            sequenceParam.Value = sequenceTable;

            uow.ExecuteSp("EXEC usp_InsertRDS3Visit" +
                " @VisitStatus, @VisitStartTime, @VisitEndTime, @VisitDuration, @SeqExecs, @Results, " +
                "@Product, @Cell, @Company, @Location, @Plant, @Process, @ProductType, @ResultDescriptions, " +
                "@Sequences, @Tester, @Zone, @VisitXml"
                , visitStatusParam, visitStartTimeParam, visitEndTimeParam, visitDurationParam, seqExecParam,
                resultParam, productParam, cellParam, companyParam, locationParam, plantParam, processParam,
                productTypeParam, resultDescriptionParam, sequenceParam, testerParam, zoneParam, visitXmlParam);
        }

        #endregion

        #region IDisposable Interface

        public void Dispose()
        {
            uow.Dispose();
            resultTable.Dispose();
            resultDescriptionTable.Dispose();
            sequenceTable.Dispose();
            sequenceExecutionsTable.Dispose();
        }

        #endregion
    }
}

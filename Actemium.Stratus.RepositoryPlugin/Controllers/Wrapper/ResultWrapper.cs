using Actemium.Stratus.DataObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Wrapper
{
    [DebuggerDisplay("{Result.ResultDescription.Name} Type={Result.Type} Status={CurrentStatus}")]
    public class ResultWrapper
    {
        #region Properties

        public Result Result { get; set; }
        public IEnumerable<ResultWrapper> ChildResults { get; set; }
        public int Level { get; set; }
        public ResultStatus? CurrentStatus
        {
            get
            {
                if (Result.Type == ResultType.Parent)
                {
                    return ChildResults != null ? ChildResults.Any(r => r.CurrentStatus == ResultStatus.Fail) ? ResultStatus.Fail : ResultStatus.Pass : Result.Status;
                }
                else
                {
                    return Result.Status;
                }
            }
        }

        #endregion

        #region Public Methods

        public XElement GetRds3()
        {
            List<XElement> t = null;

            if (ChildResults != null)
            {
                t = ChildResults.Select(r => r.GetRds3()).ToList();
            }
            return ConvertResult(children: t);

        }

        #endregion

        #region Private Methods

        private XElement ConvertResult(List<XElement> children = null)
        {
            XElement ret = null;
            string levelP = new string('P', Level);

            switch (Result.Type)
            {
                case ResultType.Grouped:
                    ret = new XElement("V" + levelP + "G", new XAttribute("name", Result.ResultDescription.Name),
                        CurrentStatus == ResultStatus.Pass ? "P" : "F");
                    break;
                case ResultType.Parent:
                    ret = new XElement("V" + levelP + "P", new XAttribute("name", Result.ResultDescription.Name),
                        new XAttribute("res", CurrentStatus == ResultStatus.Pass ? "P" : "F"), children);
                    break;
                case ResultType.Boolean:
                    ret = new XElement("V" + levelP + "B", new XAttribute("name", Result.ResultDescription.Name),
                        CurrentStatus == ResultStatus.Pass ? "P" : "F");
                    break;
                case ResultType.Operator:
                    ret = new XElement("V" + levelP + "O", new XAttribute("name", Result.ResultDescription.Name),
                        CurrentStatus == ResultStatus.Pass ? "P" : "F");
                    break;
                case ResultType.Numeric:
                    ret = new XElement("V" + levelP + "N", new XAttribute("name", Result.ResultDescription.Name),
                        (CurrentStatus == ResultStatus.Pass ? "P" : "F") + "," + Result.Value);
                    break;
                case ResultType.Bounded:
                    ret = new XElement("V" + levelP + (Result.IsFixed.Value ? "F" : "V"), new XAttribute("name", Result.ResultDescription.Name),
                         (CurrentStatus == ResultStatus.Pass ? "P" : "F") + "," + Result.Value + "," + Result.LowerLimit + "," + Result.UpperLimit + "," + Result.Units);
                    break;
                case ResultType.Array:
                    ret = new XElement("V" + levelP + "A", new XAttribute("name", Result.ResultDescription.Name), Result.Value);
                    break;
                case ResultType.String:
                    ret = new XElement("V" + levelP + (!Result.IsHidden.Value ? "S" : "H"), new XAttribute("name", Result.ResultDescription.Name),
                        Result.Value);
                    break;
                case ResultType.DataFile:
                    ret = new XElement("V" + levelP + "D", new XAttribute("name", Result.ResultDescription.Name),
                        new XAttribute("type", Result.DataType), Result.Value);
                    break;
                default:
                    break;
            }
            return ret;
        }

        #endregion
    }
}

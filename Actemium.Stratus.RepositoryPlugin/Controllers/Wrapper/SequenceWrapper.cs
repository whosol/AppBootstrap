using Actemium.Stratus.DataObjects;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Wrapper
{
    [DebuggerDisplay("{Name} Status={CurrentStatus}")]
    public class SequenceWrapper
    {
        #region Properties

        public IEnumerable<SequenceExecution> Executions;
        public IEnumerable<ResultWrapper> Results { get; set; }
        public string Name { get; set; }
        public SequenceStatus CurrentStatus
        {
            get
            {
                return Results.Any(r => r.CurrentStatus == ResultStatus.Fail) ? SequenceStatus.Fail : SequenceStatus.Pass;
            }
        }

        #endregion

        public XElement GetRds3(bool sequenceVisitInfo)
        {
            return new XElement("SEQUENCE",
                new XAttribute("name", Name),
                new XAttribute("flags", Results.Any(r => r.CurrentStatus == ResultStatus.Fail) ? "F" : "P"),
                    sequenceVisitInfo ? new XElement("VISITS", Executions.Select(se =>
                            new XElement("PF",
                            new XAttribute("date", se.StartTime.ToString("dd-MMM-yyyy HH:mm:ss")),
                            new XAttribute("flags", se.Status == SequenceStatus.Pass ? "P" : "F"),
                            new XAttribute("name", se.Visit.Process.Name)))) : null,
                                new XElement("RESULTS", Results.Select(r => r.GetRds3()).ToList()));
        }
    }
}

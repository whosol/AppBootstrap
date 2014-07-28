using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ZoneDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<ProcessDto> Processes { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<CellDto> Cells { get; set; }

        public bool ShouldSerializeProcesses()
        {
            return null != this.Processes && this.Processes.Any();
        }

        public bool ShouldSerializeCells()
        {
            return null != this.Cells && this.Cells.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class TesterDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public string Description { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<PlantDto> Plants { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
        public IEnumerable<ProcessDto> Processes { get; set; }

        public bool ShouldSerializePlants()
        {
            return null != this.Plants && this.Plants.Any();
        }

        public bool ShouldSerializeProcesses()
        {
            return null != this.Processes && this.Processes.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}

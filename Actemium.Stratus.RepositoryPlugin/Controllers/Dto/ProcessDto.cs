using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "Process")]
    public class ProcessDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<TesterDto> Testers { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<ZoneDto> Zones { get; set; }

        public bool ShouldSerializeTesters()
        {
            return null != this.Testers && this.Testers.Any();
        }

        public bool ShouldSerializeZones()
        {
            return null != this.Zones && this.Zones.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
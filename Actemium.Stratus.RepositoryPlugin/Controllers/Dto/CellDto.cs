using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "Cell")]
    public class CellDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<LocationDto> Locations { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<ZoneDto> Zones { get; set; }

        public bool ShouldSerializeLocations()
        {
            return null != this.Locations && this.Locations.Any();
        }

        public bool ShouldSerializeZones()
        {
            return null != this.Zones && this.Zones.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
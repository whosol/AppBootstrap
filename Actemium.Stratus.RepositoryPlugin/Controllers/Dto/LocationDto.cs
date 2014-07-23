using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class LocationDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<CellDto> Cells { get; set; }

        public bool ShouldSerializeCells()
        {
            return null != this.Cells && this.Cells.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
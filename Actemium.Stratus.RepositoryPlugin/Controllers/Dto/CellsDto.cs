using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class CellsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<CellDto> Cells { get; set; }

    }
}
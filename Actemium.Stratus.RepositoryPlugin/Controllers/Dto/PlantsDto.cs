using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class PlantsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<PlantDto> Plants { get; set; }
    }
}
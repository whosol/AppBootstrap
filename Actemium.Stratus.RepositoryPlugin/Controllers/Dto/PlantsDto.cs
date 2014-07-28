using System.Collections.Generic;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class PlantsDto : BaseCollectionDto
    {
        public PlantsDto()
        {
            controllerName = "plants";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<PlantDto> Plants { get; set; }
    }
}
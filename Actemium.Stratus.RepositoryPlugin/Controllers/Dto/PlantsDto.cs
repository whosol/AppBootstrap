using Actemium.Stratus.Contracts.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
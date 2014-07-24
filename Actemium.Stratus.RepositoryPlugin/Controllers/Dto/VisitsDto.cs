using Actemium.Stratus.Contracts.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class VisitsDto : BaseCollectionDto
    {
        public VisitsDto()
        {
            controllerName = "visits";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ProcessesDto : BaseCollectionDto
    {
        public ProcessesDto()
        {
            controllerName = "processes";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProcessDto> Processes { get; set; }

    }
}
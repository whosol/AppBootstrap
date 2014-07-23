using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ProcessesDto : WrapperBaseDto
    {
        public ProcessesDto()
        {
            controllerName = "processes";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProcessDto> Processes { get; set; }

    }
}
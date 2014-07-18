using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class ProcessesDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProcessDto> Processes { get; set; }

    }
}
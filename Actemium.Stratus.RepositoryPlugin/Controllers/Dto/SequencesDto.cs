using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class SequencesDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<SequenceDto> Sequences { get; set; }

    }
}
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class ResultsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ResultDto> Results { get; set; }
        [DataMember]
        public int Total { get; set; }
    }
}

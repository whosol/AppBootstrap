using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class VisitsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<VisitDto> Visits { get; set; }
        [DataMember]
        public int Count { get; set; }
    }
}
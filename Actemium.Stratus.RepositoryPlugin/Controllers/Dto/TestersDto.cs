using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class TestersDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<TesterDto> Testers { get; set; }
    }
}
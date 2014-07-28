using System.Collections.Generic;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class TestersDto : BaseCollectionDto
    {
        public TestersDto()
        {
            controllerName = "testers";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<TesterDto> Testers { get; set; }
    }
}
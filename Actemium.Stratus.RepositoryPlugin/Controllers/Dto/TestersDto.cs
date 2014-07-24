using Actemium.Stratus.Contracts.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
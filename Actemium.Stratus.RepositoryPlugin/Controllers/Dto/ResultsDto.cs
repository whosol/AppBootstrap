using System.Collections.Generic;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ResultsDto : BaseCollectionDto
    {
        public ResultsDto()
        {
            controllerName = "results";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ResultDto> Results { get; set; }
    }
}

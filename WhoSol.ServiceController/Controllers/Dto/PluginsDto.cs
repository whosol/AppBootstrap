
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace WhoSol.ServiceController.Controllers.Dto
{
    [DataContract(Namespace = "")]
    public class PluginsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<PluginDto> Plugins { get; set; }
    }
}

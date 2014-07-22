using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.ServiceController.Controllers.Dto
{
    [DataContract]
    public class PluginDto
    {
        [DataMember(Order=1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Description { get; set; }
        [DataMember(Order = 3)]
        public string Version { get; set; }
        [DataMember(Order = 4)]
        public string FileVersion { get; set; }
        [DataMember(Order = 5)]
        public string Location { get; set; }
    }
}

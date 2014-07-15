using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.ServiceController.Controllers.Dto
{
    [DataContract]
    public class Plugin
    {
        [DataMember(Order=1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Description { get; set; }
    }
}

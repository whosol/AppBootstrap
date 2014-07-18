using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Actemium.Stratus.ThirdParty.Controllers
{
    [DataContract]
   public class ThirdPartyLibrary
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Location { get; set; }
    }
}

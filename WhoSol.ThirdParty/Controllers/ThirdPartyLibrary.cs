﻿using System.Runtime.Serialization;

namespace WhoSol.ThirdParty.Controllers
{
    [DataContract(Namespace="")]
   public class ThirdPartyLibrary
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Version { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public string FileVersion { get; set; }
    }
}

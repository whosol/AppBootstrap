using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Actemium.Stratus.Contracts;
using System.Runtime.Serialization;

namespace Actemium.Stratus.ThirdParty.Controllers
{

    [DataContract]
    public class ThirdPartyLibraries
    {
        [DataMember]
        public ThirdPartyLibrary[] Libraries { get; set; }
    }
}

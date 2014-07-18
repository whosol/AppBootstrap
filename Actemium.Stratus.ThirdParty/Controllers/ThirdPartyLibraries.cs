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

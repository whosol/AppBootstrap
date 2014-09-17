using System.Runtime.Serialization;

namespace WhoSol.ThirdParty.Controllers
{

    [DataContract(Namespace="")]
    public class ThirdPartyLibraries
    {
        [DataMember]
        public ThirdPartyLibrary[] Libraries { get; set; }
    }
}

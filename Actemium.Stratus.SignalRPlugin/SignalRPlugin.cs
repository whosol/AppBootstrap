using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.SignalRPlugin
{
    public class SignalRPlugin : PluginBase
    {
        public override string Description
        {
            get { return "SignalR plugin for service controller"; }
        }

        public SignalRPlugin(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}

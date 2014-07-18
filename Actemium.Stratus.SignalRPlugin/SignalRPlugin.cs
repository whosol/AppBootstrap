using Actemium.Stratus.Contracts;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.SignalRPlugin
{
    public class SignalRPlugin : PluginBase
    {
        public override string Description
        {
            get { return "SignalR plugin for service controller"; }
        }

        public SignalRPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {

        }
    }
}

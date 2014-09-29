using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using Ninject.Extensions.Logging;

namespace WhoSol.SignalRPlugin
{
    public class SignalRPlugin : BasePlugin
    {
        public override string Description
        {
            get { return "SignalR plugin for service controller"; }
        }

        public SignalRPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            Autostart = false;
        }
    }
}

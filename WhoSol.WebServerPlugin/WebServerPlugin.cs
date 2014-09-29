using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using Ninject.Extensions.Logging;
using System;

namespace WhoSol.WebServerPlugin
{
    public class WebServerPlugin : BasePlugin
    {
        public override string Description
        {
            get { return "WebServer plugin for service controller"; }
        }

        public WebServerPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            Autostart = false;
        }
    }
}

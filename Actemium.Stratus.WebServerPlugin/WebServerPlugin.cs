using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Ninject.Extensions.Logging;
using System;

namespace Actemium.Stratus.WebServerPlugin
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
        }
    }
}

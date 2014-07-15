using Actemium.Stratus.Contracts;
using System;

namespace Actemium.Stratus.WebServerPlugin
{
    public class WebServerPlugin : PluginBase
    {
        public override string Description
        {
            get { return "WebServer plugin for service controller"; }
        }

        public WebServerPlugin(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

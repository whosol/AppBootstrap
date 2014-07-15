using Actemium.Stratus.Contracts;
using Microsoft.Owin.Hosting;
using System;

namespace Actemium.Stratus.OwinSelfHostPlugin
{
    public class OwinSelfHostPlugin : PluginBase
    {
        private IDisposable owinServer;
        private OwinSelfHostStartup startup;

        public override string Description
        {
            get { return "OWIN Self Host Plugin"; }
        }

        public override void Start()
        {
            owinServer = WebApp.Start(string.Format("http://*:{0}", configuration.Get<int>("WebServerPort")), this.startup.Configuration);
        }

        public OwinSelfHostPlugin(IConfiguration configuration, OwinSelfHostStartup startup)
            : base(configuration)
        {
            this.startup = startup;
            configuration.Set("WebServerPort", 8080);
        }
    }
}

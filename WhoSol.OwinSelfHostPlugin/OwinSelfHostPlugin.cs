using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using WhoSol.OwinSelfHostPlugin.Enums;
using WhoSol.Utilities;
using Microsoft.Owin.Hosting;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WhoSol.OwinSelfHostPlugin
{
    public class OwinSelfHostPlugin : BasePlugin
    {
        private IDisposable owinServer;
        private readonly OwinSelfHostStartup startup;
        private readonly Dictionary<ConfigSection, Dictionary<ConfigKey, object>> moduleConfig;

        public OwinSelfHostPlugin(ILogger logger, IConfiguration configuration, OwinSelfHostStartup startup)
            : base(logger, configuration)
        {
            this.startup = startup;
            this.moduleConfig = ConfigFile<ConfigSection, ConfigKey>.Parse(this, configuration.Get<string>("PluginDirectory"));
            configuration.Set("OwinSelfHost", this.moduleConfig);
        }

        public override string Description
        {
            get { return "OWIN Self Host Plugin"; }
        }

        public override void Start()
        {
            owinServer = WebApp.Start(
                string.Format("http://*:{0}", 
                moduleConfig[ConfigSection.WebServer][ConfigKey.Port]), 
                this.startup.Configuration);
        }

        public override void Stop()
        {
            owinServer.Dispose();
        }

      
    }
}

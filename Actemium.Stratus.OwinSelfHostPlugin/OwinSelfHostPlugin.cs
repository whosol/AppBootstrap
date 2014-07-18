using Actemium.Stratus.Contracts;
using Actemium.Stratus.OwinSelfHostPlugin.Enums;
using Actemium.Stratus.Utilities;
using Microsoft.Owin.Hosting;
using Ninject.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

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
            owinServer = WebApp.Start(string.Format("http://*:{0}", configuration.GetModuleConfig<ConfigSection,ConfigKey>("OwinSelfHost")[ConfigSection.WebServer][ConfigKey.Port]), this.startup.Configuration);
        }

        public OwinSelfHostPlugin(ILogger logger, IConfiguration configuration, OwinSelfHostStartup startup)
            : base(logger, configuration)
        {
            this.startup = startup;
            configuration.Set("OwinSelfHost", ConfigFile<ConfigSection, ConfigKey>.Parse(this, configuration.Get<string>("PluginDirectory"), "PluginSettings"));
        }
    }
}

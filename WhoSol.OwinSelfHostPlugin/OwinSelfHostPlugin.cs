using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using WhoSol.OwinSelfHostPlugin.Enums;
using WhoSol.Utilities;
using Microsoft.Owin.Hosting;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using WhoSol.Contracts.Constants;

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
            this.moduleConfig = ConfigFile<ConfigSection, ConfigKey>.Parse(this, configuration.Get<string>(Config.PluginDirectory));
            configuration.Set("OwinSelfHost", this.moduleConfig);
        }

        public override string Description
        {
            get { return "OWIN Self Host Plugin"; }
        }

        public override void Start(params object[] args)
        {
            int? port = 0;
            if (args.Length == 0)
            {
                port = int.Parse(moduleConfig[ConfigSection.WebServer][ConfigKey.Port] as string);

            }
            else if (args.Length == 1)
            {
                port = args[0] as int?;
                if (port == null)
                {
                    throw new ArgumentException("Port is in incorrect format");
                }
            }

            if (port != null)
            {
                logger.Info("Opening OWIN Server on port {0}", port);
                owinServer = WebApp.Start(string.Format("http://*:{0}", port), this.startup.Configuration);
            }
            else
            {
                throw new ApplicationException("Cannot open OWIN server as no port is specified");
            }
        }
        public override void Stop()
        {
            if (owinServer != null)
            {
                owinServer.Dispose();
            }
        }
    }
}

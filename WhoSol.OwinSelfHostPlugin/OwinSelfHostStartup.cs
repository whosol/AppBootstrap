using WhoSol.Contracts;
using Ninject.Extensions.Logging;
using Owin;
using Microsoft.Owin.Cors;
using System;

namespace WhoSol.OwinSelfHostPlugin
{
    public class OwinSelfHostStartup : IStartup
    {
        private readonly IStartup[] serverStartups;
        private readonly ILogger logger;

        public OwinSelfHostStartup(ILogger logger, IStartup[] serverStartups)
        {
            this.logger = logger;
            this.serverStartups = serverStartups;
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);

            logger.Info("OWIN Self Host Plugin Started");

            try
            {
                foreach (var startup in serverStartups)
                {
                    startup.Configuration(app);
                }

                logger.Info("Installed OWIN Plugins");
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Plugin failed to load. Exception: ({0})", ex.Message));
            }
        }
    }
}

using WhoSol.Contracts;
using Ninject.Extensions.Logging;
using Owin;

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
            foreach (var startup in serverStartups)
            {
                startup.Configuration(app);
            }
        }
    }
}

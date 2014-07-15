using Actemium.Stratus.Contracts;
using Owin;

namespace Actemium.Stratus.OwinSelfHostPlugin
{
    public class OwinSelfHostStartup : IStartup
    {
        private readonly IStartup[] serverStartups;

        public OwinSelfHostStartup(IStartup[] serverStartups)
        {
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

using WhoSol.Contracts;
using Owin;
using System;
using Ninject.Extensions.Logging;

namespace WhoSol.SignalRPlugin
{
    public class SignalRStartup : IStartup
    {
        private readonly ILogger logger;

        public SignalRStartup(ILogger logger)
        {
            this.logger = logger;
        }

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            logger.Info("SignalR Plugin Started");

        }
    }
}

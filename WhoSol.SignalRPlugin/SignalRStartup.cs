using WhoSol.Contracts;
using Owin;
using System;

namespace WhoSol.SignalRPlugin
{
    public class SignalRStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

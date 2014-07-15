using Actemium.Stratus.Contracts;
using Owin;
using System;

namespace Actemium.Stratus.SignalRPlugin
{
    public class SignalRStartup : IStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

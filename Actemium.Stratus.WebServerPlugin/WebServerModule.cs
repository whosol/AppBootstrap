using System;
using System.Linq;
using Ninject.Modules;
using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.WebServerPlugin
{
    public class WebServerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<WebServerPlugin>().InSingletonScope();
            Bind<IStartup>().To<WebServerStartup>().InSingletonScope();
        }
    }
}

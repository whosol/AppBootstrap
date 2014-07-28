using System;
using System.Linq;
using Ninject.Modules;
using WhoSol.Contracts;

namespace WhoSol.WebServerPlugin
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

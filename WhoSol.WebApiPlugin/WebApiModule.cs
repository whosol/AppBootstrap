using WhoSol.Contracts;
using Ninject.Modules;
using System;

namespace WhoSol.WebApiPlugin
{
    public class WebApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<WebApiPlugin>().InSingletonScope();
            Bind<IStartup>().To<WebApiStartup>().InSingletonScope();
        }
    }
}

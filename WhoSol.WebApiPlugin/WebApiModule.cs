using WhoSol.Contracts;
using Ninject.Modules;
using System;

namespace WhoSol.WebApiPlugin
{
    public class WebApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<WebApiPlugin>();
            Bind<IStartup>().To<WebApiStartup>();
        }
    }
}

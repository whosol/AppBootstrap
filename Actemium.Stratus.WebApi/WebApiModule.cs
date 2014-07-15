using Actemium.Stratus.Contracts;
using Ninject.Modules;
using System;

namespace Actemium.Stratus.WebApi
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

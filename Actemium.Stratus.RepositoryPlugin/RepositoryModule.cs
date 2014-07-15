using Actemium.Stratus.Contracts;
using Ninject.Modules;
using System;

namespace Actemium.Stratus.RepositoryPlugin
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<StratusUnitOfWork>();
            Bind<IPlugin>().To<RepositoryPlugin>();
        }
    }
}

using WhoSol.Contracts;
using Ninject.Modules;
using System;
using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.RepositoryPlugin
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStratusUnitOfWork>().To<StratusUnitOfWork>();
            Bind<IPlugin>().To<RepositoryPlugin>();
        }
    }
}

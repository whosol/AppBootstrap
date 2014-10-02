using Ninject.Modules;
using System;
using System.Linq;
using WhoSol.Contracts;
using WhoSol.Contracts.Base;

namespace WhoSol.EntityFrameworkPlugin
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<EntityFrameworkPlugin>();
            Bind<IUnitOfWork>().To<EntityFrameworkUnitOfWork>().Named("EntityFramework");
            Bind(typeof(IRepository<>)).To(typeof(EntityFrameworkRepository<>)).Named("EntityFramework");
        }
    }
}

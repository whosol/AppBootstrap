using Ninject.Modules;
using WhoSol.Contracts;

namespace WhoSol.XMLDBPlugin
{
    public class XMLDBModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<XMLDBPlugin>();
            Bind<IUnitOfWork>().To<XMLDBUnitOfWork>().Named("XMLDB");
            Bind(typeof(IRepository<>)).To(typeof(XMLDBRepository<>)).Named("XMLDB");
        }
    }
}

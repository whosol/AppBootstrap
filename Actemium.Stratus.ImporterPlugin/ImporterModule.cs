using WhoSol.Contracts;
using Ninject.Modules;

namespace Actemium.Stratus.ImporterPlugin
{
    public class ImporterModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IImporter>().To<Rds3Importer>();
            Bind<IPlugin>().To<ImporterPlugin>();
        }
    }
}

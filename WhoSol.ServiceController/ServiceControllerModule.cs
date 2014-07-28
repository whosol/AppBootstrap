using WhoSol.Contracts;
using Ninject.Modules;

namespace WhoSol.ServiceController
{
    public class ServiceControllerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<Configuration>().InSingletonScope();
            Bind<IController>().To<ServiceControllerMain>().InSingletonScope();
        }
    }
}

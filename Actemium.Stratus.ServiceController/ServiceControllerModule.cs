using Actemium.Stratus.Contracts;
using Ninject.Modules;

namespace Actemium.Stratus.ServiceController
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

using Actemium.Stratus.Contracts;
using Ninject.Modules;

namespace Actemium.Stratus.OwinSelfHostPlugin
{
    public class OwinSelfHostModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<OwinSelfHostPlugin>().InSingletonScope();
        }
    }
}

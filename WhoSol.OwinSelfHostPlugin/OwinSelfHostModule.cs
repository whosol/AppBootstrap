using WhoSol.Contracts;
using Ninject.Modules;

namespace WhoSol.OwinSelfHostPlugin
{
    public class OwinSelfHostModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<OwinSelfHostPlugin>().Named("OWIN");
        }
    }
}

using WhoSol.Contracts;
using Ninject.Modules;

namespace WhoSol.SignalRPlugin
{
    public class SignalRModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<SignalRPlugin>();
            Bind<IStartup>().To<SignalRStartup>();
        }
    }
}

using Actemium.Stratus.Contracts;
using Ninject.Modules;

namespace Actemium.Stratus.SignalRPlugin
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

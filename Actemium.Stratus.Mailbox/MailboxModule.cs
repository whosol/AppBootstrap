using Actemium.Stratus.Contracts;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Manage;
using Ninject.Modules;

namespace Actemium.Stratus.Mailbox
{
    public class MailboxModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<MailboxPlugin>().InSingletonScope();
            Bind<IShopFloorResultsExtension>().To<ConfigManager>().InSingletonScope();
        }
    }
}

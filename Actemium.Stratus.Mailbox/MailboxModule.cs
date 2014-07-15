using Actemium.Stratus.Contracts;
using Ninject.Modules;

namespace Actemium.Stratus.Mailbox
{
    public class MailboxModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<MailboxPlugin>().InSingletonScope();
        }
    }
}

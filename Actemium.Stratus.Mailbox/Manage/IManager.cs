using Actemium.Stratus.MailboxPlugin.Events;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public interface IManager : IComponentBase
    {
        [EventSubscription("topic://ConfigChanged", typeof(OnBackground))]
        void ConfigChangedHandler(object sender, ConfigChangedEventArgs e);
    }
}

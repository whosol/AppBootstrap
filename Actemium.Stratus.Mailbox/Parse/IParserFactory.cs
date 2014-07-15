using Actemium.Stratus.MailboxPlugin.Events;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;

namespace ShopFloorResults.Parse
{
    public interface IParserFactory
    {
        [EventSubscription("topic://ConfigChanged", typeof(OnBackground))]
        void ConfigChangedHandler(object sender, ConfigChangedEventArgs e);
    }
}
 
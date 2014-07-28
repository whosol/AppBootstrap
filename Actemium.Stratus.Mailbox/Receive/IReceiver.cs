using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Parse;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Receive
{
    public interface IReceiver : IComponentBase
    {
        [EventPublication("topic://XDocumentAvailable", HandlerRestriction.Synchronous)]
        event EventHandler<XDocumentAvailableEventArgs> XDocumentAvailableEvent;

        [EventPublication("topic://ParseStringToXml", HandlerRestriction.Synchronous)]
        event EventHandler<ParseStringToXmlEventArgs> ParseStringToXmlRequest;

        [EventSubscription("topic://ConfigChanged", typeof(OnBackground))]
        void ConfigChangedHandler(object sender, ConfigChangedEventArgs e);
    }
}

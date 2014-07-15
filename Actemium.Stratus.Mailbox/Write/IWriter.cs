using Actemium.Stratus.MailboxPlugin.Events;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Write
{
    public interface IWriter : IComponentBase
    {
        [EventSubscription("topic://XDocumentAvailable", typeof(OnPublisher))]
        void XDocumentAvailableHandler(object sender, XDocumentAvailableEventArgs e);

        [EventSubscription("topic://ConfigChanged", typeof(OnBackground))]
        void ConfigChangedHandler(object sender, ConfigChangedEventArgs e);
    }
}

using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Parse;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Respond
{
    public interface IResponder : IComponentBase
    {
        [EventSubscription("topic://ConfigChanged", typeof(OnBackground))]
        void ConfigChangedHandler(object sender, ConfigChangedEventArgs e);

        [EventPublication("topic://ParseStringToXml", HandlerRestriction.Synchronous)]
        event EventHandler<ParseStringToXmlEventArgs> ParseStringToXmlRequest;
    }
}

using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public interface IParser : IComponentBase
    {
        [EventSubscription("topic://ParseStringToXml", typeof(OnPublisher))]
        void ParseStringToXml(object sender, ParseStringToXmlEventArgs e);
    }
}

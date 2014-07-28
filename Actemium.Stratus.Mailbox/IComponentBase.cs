using Actemium.Stratus.MailboxPlugin.Events;
using Appccelerate.EventBroker;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin
{
    public interface IComponentBase
    {
        [EventPublication("topic://Log", HandlerRestriction.Asynchronous)]
        event EventHandler<LogEventArgs> LogEvent;
    }
}

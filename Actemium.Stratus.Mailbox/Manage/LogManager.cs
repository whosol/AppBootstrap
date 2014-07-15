using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Appccelerate.Bootstrapper;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Diagnostics;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public class LogManager : ShopFloorResultsExtensionBase, IManager
    {
        public LogManager()
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new EventLogTraceListener("Mailbox"));
        }

        [EventSubscription("topic://Log", typeof(OnBackground))]
        public void LogHandler(object sender, LogEventArgs e)
        {
            Trace.WriteLine(string.Format("{0}: {1}", e.Time.TimeOfDay, e.Message), e.Level.ToString());
        }


        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            if (e.ChangedSection != ConfigSection.Logging)
                return;


        }

        public override string Describe()
        {
            return "Log Manager";
        }


        public event EventHandler<LogEventArgs> LogEvent;

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            Trace.Close();
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });
        }
    }
}

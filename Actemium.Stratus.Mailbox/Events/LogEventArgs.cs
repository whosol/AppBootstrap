using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Events
{
    public class LogEventArgs : EventArgs
    {
        public DateTime Time { get; private set; }

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public LogEventArgs()
        {
            Time = DateTime.Now;
        }
    }
}

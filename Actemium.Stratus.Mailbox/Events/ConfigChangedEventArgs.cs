using System;
using System.Collections.Generic;
using System.Linq;
using Actemium.Stratus.MailboxPlugin.Enums;

namespace Actemium.Stratus.MailboxPlugin.Events
{
    public class ConfigChangedEventArgs : EventArgs
    {
        #region Properties

        public ConfigSection ChangedSection { get; set; }

        public Dictionary<ConfigKey, object> Configuration { get; set; } 
        
        #endregion
    }
}

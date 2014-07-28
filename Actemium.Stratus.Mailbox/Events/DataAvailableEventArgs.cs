using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Events
{
    public class DataAvailableEventArgs : EventArgs
    {
        #region Properties

        public string Data { get; set; } 
        
        #endregion
    }
}

using Actemium.Stratus.Contracts.Enums;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Events
{
    public class XDocumentAvailableEventArgs : EventArgs
    {
        #region Properties
        
        public XDocument Data { get; set; } 

        #endregion

        public PersistStatus PersistStatus { get; set; }
    }
}

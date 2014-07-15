using System;
using System.Linq;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public class ParseStringToXmlEventArgs : EventArgs
    {
        public string Data { get; set; }
        public XDocument Xml { get; set; }
    }
}

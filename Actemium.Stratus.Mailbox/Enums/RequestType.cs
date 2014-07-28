using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Enums
{
    public enum RequestType : byte
    {
        Acknowledge = (byte)'A',
        ClientRequest = (byte)'V',
        ClientRequestNoHistory = (byte)'v',
    }
}

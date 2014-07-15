using Actemium.Stratus.MailboxPlugin.Enums;
using System;
using System.Linq;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public class ResultTcpPacket
    {
        public ushort Size { get; set; }

        public string Data { get; set; }

        public uint Checksum { get; set; }

        public RequestType RequestType { get; set; }
    }
}

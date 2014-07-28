using System;
using System.Net.Sockets;
using Actemium.Stratus.MailboxPlugin.Enums;

namespace Actemium.Stratus.MailboxPlugin.Events
{
    public class TcpStreamReceivedEventArgs : EventArgs
    {
        #region Properties

        public string Data { get; set; }

        public NetworkStream Stream { get; set; }

        public PacketType PacketType { get; set; }
        
        #endregion
    }
}

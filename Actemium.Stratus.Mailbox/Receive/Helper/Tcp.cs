using Actemium.Stratus.MailboxPlugin.Enums;
using System.Net.Sockets;

namespace Actemium.Stratus.MailboxPlugin.Receive.Helper
{
    public static class Tcp
    {
        public static void SendAcknowledge(NetworkStream stream, ResponseCode extraInfo)
        {
            //Send an acknowledge to the results source
            //var ack = new byte[] { (byte)ResultProtocol.Acknowledge, (byte)extraInfo, 0, 0 };            
            var ack = new byte[] { (byte)RequestType.Acknowledge, 0, 0, 0 };
            stream.Write(ack, 0, ack.Length);
        }
    }
}

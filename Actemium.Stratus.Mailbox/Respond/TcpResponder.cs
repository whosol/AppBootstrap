using System;
using System.Linq;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Parse;
using Actemium.Stratus.MailboxPlugin.Receive.Helper;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Respond
{
    public class TcpResponder : ShopFloorResultsExtensionBase, IResponder
    {
        #region Fields
        
        private string resultsServerUrl;

        #endregion

        #region Event Handlers

        [EventSubscription("topic://TcpStreamReceived", typeof(OnPublisher))]
        public void TcpStreamReceivedHandler(object sender, TcpStreamReceivedEventArgs e)
        {
            if (e.PacketType == PacketType.ResultRequest)
            {
                //Tcp.SendAcknowledge(e.Stream, ResponseCode.ResultResponsePending);

                var parseEventArgs = new ParseStringToXmlEventArgs{Data = e.Data};
                
                ParseStringToXmlRequest(this, parseEventArgs);

                var xml = parseEventArgs.Xml;
                    
                    //ParserFactory.GetParser(e.Data).Parse();

                var productUniqueId = (string)xml.Element("REQUEST").Attribute("vin");

                var sequences = new StringBuilder();

                foreach (var sequenceElement in xml.Element("REQUEST").Elements())
                {
                    sequences.AppendFormat("&sequences={0}", (string)sequenceElement.Attribute("name"));
                }


                var request = (HttpWebRequest)WebRequest.Create(string.Format(resultsServerUrl + "/results?productUniqueId={0}{1}", productUniqueId, sequences.ToString()));

                request.Method = "GET";
                request.ContentType = "application/xml";

                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var buffer = new byte[response.ContentLength];

                            var data = XDocument.Load(response.GetResponseStream());

                            buffer = new byte[response.ContentLength + 8];
                            buffer[0] = (byte)'v';

                            BitConverter.GetBytes((Int16)response.ContentLength).Reverse().ToArray().CopyTo(buffer, 2);
                            var checksum = Checksum.CalculateResultChecksum(data.ToString(), (int)response.ContentLength);

                            BitConverter.GetBytes(checksum).Reverse().ToArray().CopyTo(buffer, buffer.Length - 4);

                            Encoding.UTF8.GetBytes(data.ToString()).CopyTo(buffer, 4);

                            e.Stream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
                catch
                {
                    Tcp.SendAcknowledge(e.Stream, ResponseCode.RejectRequest);
                }
            }
        } 

        #endregion
        
        #region IResponder Interface

        public void ConfigChangedHandler(object sender, Actemium.Stratus.MailboxPlugin.Events.ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.Network)
            {
                resultsServerUrl = (string)e.Configuration[ConfigKey.ResultsServerUrl];
            }
        }
        
        public event EventHandler<ParseStringToXmlEventArgs> ParseStringToXmlRequest;

        public event EventHandler<LogEventArgs> LogEvent;

        #endregion
        
        #region Public Overrides

        public override string Describe()
        {
            return "TCP Responder";
        } 

        #endregion


        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });
        }


    }
}

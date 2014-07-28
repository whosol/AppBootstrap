using WhoSol.Contracts.Enums;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Parse;
using Actemium.Stratus.MailboxPlugin.Receive.Helper;
using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Receive
{
    public sealed class TcpReceiver : ShopFloorResultsExtensionBase, IReceiver, IDisposable
    {
        #region Fields

        private readonly BlockingCollection<XDocument> queue = new BlockingCollection<XDocument>();
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private Task dequeueTask;

        #endregion

        #region Event Handlers

        [EventSubscription("topic://TcpStreamReceived", typeof(OnPublisher))]
        public void TcpStreamReceivedHandler(object sender, TcpStreamReceivedEventArgs e)
        {
            if (e.PacketType == PacketType.Result)
            {
                var parseEventArgs = new ParseStringToXmlEventArgs { Data = e.Data };

                ParseStringToXmlRequest(this, parseEventArgs);

                var xml = parseEventArgs.Xml;

                if (xml != null)
                {
                    Tcp.SendAcknowledge(e.Stream, ResponseCode.AllOk);
                    queue.Add(xml);
                }
            }
        }

        #endregion

        #region Public Overrides

        public override void Start()
        {
            dequeueTask = Task.Factory.StartNew(() =>
             {
                 while (!cts.IsCancellationRequested)
                 {
                     try
                     {
                         var xml = queue.Take(cts.Token);
                         if (XDocumentAvailableEvent != null)
                         {
                             var eventArgs = new XDocumentAvailableEventArgs { Data = xml };
                             XDocumentAvailableEvent(this, eventArgs);
                             if (eventArgs.PersistStatus != PersistStatus.OK)
                             {
                                 //TODO: Put rejected files somewhere
                                 LogEvent(this, new LogEventArgs
                                 {
                                     Level = LogLevel.Information,
                                     Message = "Duplicate : " + (string)xml.Element("VEHICLE").Attribute("vin")
                                 });
                             }
                         }
                     }
                     catch (OperationCanceledException)
                     {
                         LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Dequeue task cancelled..." });
                     }
                 }
             }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        }

        public override void Stop()
        {
            cts.Cancel();
            try
            {
                Task.WaitAll(new[] { dequeueTask }, 100, cts.Token);

            }
            catch (OperationCanceledException)
            {
            }
            catch (AggregateException ae)
            {
                //Ensure the exceptions from the tasks are only cancel exceptions
                ae.Handle((x) =>
                    {
                        return x is TaskCanceledException ? true : false;
                    });
            }
        }

        public override string Describe()
        {
            return "TCP Receiver";
        }

        #endregion

        #region IReceiver Interface

        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.Network)
            {
            }
        }

        public event EventHandler<LogEventArgs> LogEvent;

        public event EventHandler<ParseStringToXmlEventArgs> ParseStringToXmlRequest;

        public event EventHandler<XDocumentAvailableEventArgs> XDocumentAvailableEvent;

        #endregion

        #region IDisposable Interface

        public void Dispose()
        {
            queue.Dispose();
            cts.Dispose();
            dequeueTask.Dispose();
        }

        #endregion
    }
}

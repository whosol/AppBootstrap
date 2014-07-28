using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Receive.Helper;
using Appccelerate.EventBroker;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public sealed class TcpManager : ShopFloorResultsExtensionBase, IManager, IDisposable
    {
        #region Events

        [EventPublication("topic://TcpStreamReceived", HandlerRestriction.Synchronous)]
        public event EventHandler<TcpStreamReceivedEventArgs> TcpStreamReceivedEvent;

        #endregion

        #region Fields

        private const int Timeout = 15;
        private const int BufferSize = 262144;
        private const int HeaderFieldLength = 2;
        private const int DataSizeFieldLength = 2;
        private const ushort ChecksumFieldLength = 4;

        private TcpListener listener;
        private int port = 4000;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private Task acceptClientsTask;

        #endregion

        #region Public Overrides

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Starting " + Describe() });
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            acceptClientsTask = AcceptClientsAsync(cts.Token);
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopping " + Describe() });
            cts.Cancel();
            try
            {
                Task.WaitAll(new[] { acceptClientsTask }, 100, cts.Token);
            }
            catch (OperationCanceledException)
            {

            }
            catch (AggregateException ae)
            {
                ae.Handle((x) =>
                {
                    if (x is TaskCanceledException || x is NullReferenceException)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });

        }

        public override string Describe()
        {
            return "TCP Connection Manager";
        }

        #endregion

        #region Private Methods

        private async Task AcceptClientsAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var client = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Information,
                        Message = "Client connected : " + client.Client.RemoteEndPoint
                    });
                    await ResultFromTesterStream(client, ct);
                }
                catch (SocketException se)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Error,
                        Message = se.Message
                    });
                }
            }
            listener.Stop();
        }

        private async Task ResultFromTesterStream(TcpClient client, CancellationToken ct)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                var tcpPacket = await ReadIntoBuffer(stream, client.Client.RemoteEndPoint.ToString(), ct).ConfigureAwait(false);

                if (tcpPacket != null)
                {
                    //Not a result or request packet so ignore and start the read again
                    if ((tcpPacket.RequestType == RequestType.ClientRequest) || (tcpPacket.RequestType == RequestType.ClientRequestNoHistory))
                    {
                        //parse the size, data and checksum from the stream
                        var calculatedChecksum = Checksum.CalculateResultChecksum(tcpPacket.Data, tcpPacket.Size);

                        //If the checsum is not valid respond to the tester
                        if (tcpPacket.Checksum != calculatedChecksum)
                        {
                            //Tcp.SendAcknowledge(stream, ResponseCode.InvalidChecksum);

                            LogEvent(this, new LogEventArgs
                            {
                                Level = LogLevel.Error,
                                Message = string.Format("Invalid checksum. Expected {0} Received {1}", calculatedChecksum, tcpPacket.Checksum)
                            });

                            return;
                        }

                        var packetType = GetPacketType(tcpPacket.Data);

                        //If the format is unknown respond to the tester
                        if (packetType == PacketType.None)
                        {
                            //Tcp.SendAcknowledge(stream, ResponseCode.UnknownXmlFormat);

                            LogEvent(this, new LogEventArgs
                            {
                                Level = LogLevel.Error,
                                Message = "Unknown Packet Type"
                            });

                            return;
                        }

                        LogEvent(this, new LogEventArgs
                        {
                            Level = LogLevel.Information,
                            Message = "Packet type received : " + packetType
                        });

                        //The data must be good so send it to the receiver or responder based on the packet type
                        TcpStreamReceivedEvent(this, new TcpStreamReceivedEventArgs
                        {
                            Data = tcpPacket.Data,
                            Stream = stream,
                            PacketType = packetType
                        });
                    }
                    else
                    {
                        //Tcp.SendAcknowledge(stream, ResponseCode.RejectRequest);

                        LogEvent(this, new LogEventArgs
                        {
                            Level = LogLevel.Error,
                            Message = "Not a request.."
                        });

                        return;
                    }
                }
                else
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Error,
                        Message = "No data read from the buffer"
                    });
                }
            }

        }

        private static ResultTcpPacket ParseBuffer(ushort size, RequestType requestType, byte[] readBuffer)
        {
            return new ResultTcpPacket
            {
                Size = size,
                RequestType = requestType,
                Data = Encoding.ASCII.GetString(readBuffer.Skip(HeaderFieldLength + DataSizeFieldLength).Take(size).ToArray()),
                Checksum = BitConverter.ToUInt32(readBuffer.Skip(size + HeaderFieldLength + DataSizeFieldLength).Take(ChecksumFieldLength).Reverse().ToArray(), 0),
            };
        }

        private async Task<ResultTcpPacket> ReadIntoBuffer(NetworkStream stream, string uid, CancellationToken ct)
        {
            var readBuffer = new byte[BufferSize];
            var amountRead = 0;
            var totalRead = 0;
            var count = 1;
            ResultTcpPacket tcpPacket = null;

            try
            {
                //Read the header bytes
                amountRead = await stream.ReadAsync(readBuffer, 0, HeaderFieldLength + DataSizeFieldLength, ct);

                LogEvent(this, new LogEventArgs
                {
                    Level = LogLevel.Information,
                    Message = string.Format("({0}) {1} header bytes read", uid, amountRead)
                });

                //If the actual amount read equals the correct header length
                if (amountRead == (HeaderFieldLength + DataSizeFieldLength))
                {
                    //Parse the request type
                    var reqType = (RequestType)readBuffer[0];
                    //Parse the data length
                    var dataFieldLength = BitConverter.ToUInt16(readBuffer.Skip(HeaderFieldLength).Take(DataSizeFieldLength).Reverse().ToArray(), 0);

                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Information,
                        Message = string.Format("({0}) Request Type : {1}, {2} data bytes expected", uid, reqType, dataFieldLength)
                    });

                    amountRead = 0;


                    while (totalRead < (dataFieldLength + ChecksumFieldLength))
                    {
                        //Configure read and timeout tasks
                        var streamReadTask = stream.ReadAsync(readBuffer, totalRead + HeaderFieldLength + DataSizeFieldLength, readBuffer.Length - (totalRead + HeaderFieldLength + DataSizeFieldLength), ct);
                        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(Timeout), ct);

                        //Wait for one of the tasks to complete
                        var completedTask = await Task.WhenAny(timeoutTask, streamReadTask).ConfigureAwait(false);

                        //if we timed out log and exit
                        if (completedTask == timeoutTask)
                        {
                            LogEvent(this, new LogEventArgs
                            {
                                Level = LogLevel.Error,
                                Message = string.Format("({0}) Timed out...", uid)
                            });

                            readBuffer = null;
                        }
                        else
                        {
                            //Get the amount of bytes read from the stream
                            amountRead = streamReadTask.Result;
                            totalRead += amountRead;
                            LogEvent(this, new LogEventArgs
                            {
                                Level = LogLevel.Information,
                                Message = string.Format("({0}) Bytes read : {1} on read {2}", uid, amountRead, count++)
                            });
                        }
                    }

                    tcpPacket = ParseBuffer(dataFieldLength, reqType, readBuffer);
                }

            }
            catch (ObjectDisposedException)
            {
                LogEvent(this, new LogEventArgs
                {
                    Level = LogLevel.Error,
                    Message = string.Format("({0}) The connection has been closed", uid)
                });

                readBuffer = null;
            }

            return tcpPacket;
        }

        private PacketType GetPacketType(string data)
        {
            var packetType = PacketType.None;

            if (data.Contains("<REQUEST rds=\"3.0\""))
            {
                packetType = PacketType.ResultRequest;
            }
            else if (data.Contains("<VEHICLE rds=\"3.0\""))
            {
                packetType = PacketType.Result;
            }
            return packetType;
        }

        #endregion

        #region IManager Interface

        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.Network)
            {
                var newPort = int.Parse(e.Configuration[ConfigKey.ReceivePort] as string);
                if (port != newPort)
                {
                    port = newPort;
                }
            }
        }

        #endregion

        #region IDisposable Interface

        public void Dispose()
        {
            cts.Dispose();
        }

        #endregion

        public event EventHandler<LogEventArgs> LogEvent;
    }
}

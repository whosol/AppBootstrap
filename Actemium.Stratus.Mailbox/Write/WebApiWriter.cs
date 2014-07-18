using Actemium.Stratus.Contracts.Enums;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace Actemium.Stratus.MailboxPlugin.Write
{
    public class WebApiWriter : ShopFloorResultsExtensionBase, IWriter
    {
        #region Fields

        private string resultsServerUrl;

        #endregion

        #region IWriter Interface

        public void XDocumentAvailableHandler(object sender, XDocumentAvailableEventArgs e)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(resultsServerUrl + "/visits/xml");

                request.Method = "POST";
                request.ContentType = "application/xml";

                var bytes = Encoding.ASCII.GetBytes(e.Data.ToString());
                request.ContentLength = bytes.Length;

                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }

                LogEvent(this, new LogEventArgs
                {
                    Level = LogLevel.Information,
                    Message = "Posted result to " + resultsServerUrl + "/visits/xml"
                });

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        LogEvent(this, new LogEventArgs
                        {
                            Level = LogLevel.Information,
                            Message = "Successfully persisted visit for " + (string)e.Data.Element("VEHICLE").Attribute("vin")
                        });
                        e.PersistStatus = PersistStatus.OK;
                    }
                }
            }
            catch (WebException we)
            {
                if (we.Response == null && we.Status == WebExceptionStatus.ConnectFailure)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Error,
                        Message = "There is no connection to the WebApi Server"
                    });
                    e.PersistStatus = PersistStatus.NoConnection;
                }
                else if (we.Response == null && we.Status == WebExceptionStatus.Timeout)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Warning,
                        Message = "The connection to the WebAPI server timed out"
                    });
                    e.PersistStatus = PersistStatus.Timeout;
                }
                else if (we.Response == null && we.Status == WebExceptionStatus.ProtocolError)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Warning,
                        Message = "Could not post result. Check WebAPI settings"
                    });
                    e.PersistStatus = PersistStatus.WebApiReject;
                }
                else if (((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.BadRequest)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Information,
                        Message = "The result was rejected by the WebAPI server due to an invalid format"
                    });
                    e.PersistStatus = PersistStatus.InvalidFormat;
                }
                else if (((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.Conflict)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Information,
                        Message = "The result was rejected by the WebAPI server as it is a duplicate"
                    });
                    e.PersistStatus = PersistStatus.Duplicate;
                }
                else if (((HttpWebResponse)we.Response).StatusCode == HttpStatusCode.InternalServerError)
                {
                    LogEvent(this, new LogEventArgs
                    {
                        Level = LogLevel.Information,
                        Message = "The result could not be persisted due to a database error. Please check the server at " + resultsServerUrl
                    });
                    e.PersistStatus = PersistStatus.DatabaseError;
                }
            }
        }

        public void ConfigChangedHandler(object sender, Events.ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.Network)
            {
                resultsServerUrl = (string)e.Configuration[ConfigKey.ResultsServerUrl];
                LogEvent(this, new LogEventArgs
                {
                    Level = LogLevel.Information,
                    Message = "The result server URI has been updated to " + resultsServerUrl
                });
            }
        }

        #endregion

        #region Public Overrides

        public override string Describe()
        {
            return "WebAPI Writer";
        }

        #endregion


        public event EventHandler<LogEventArgs> LogEvent;

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

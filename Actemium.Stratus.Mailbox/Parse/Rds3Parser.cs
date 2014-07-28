using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Events;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public class Rds3Parser : ShopFloorResultsExtensionBase, IParser
    {
        #region IParser Interface

        internal XDocument Parse(string data)
        {
            try
            {
                if (data == null)
                {
                    var message = "Data passed into RDS3 parser cannot be null";
                    LogEvent(this, new LogEventArgs { Level = LogLevel.Error, Message = message });
                    throw new ArgumentException(message);
                }
                LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "RDS3 Parser" });

                var rds3Result = XDocument.Parse(data.Trim('\0'));

                LogEvent(this, new LogEventArgs
                {
                    Level = LogLevel.Information,
                    Message = rds3Result.Element("VEHICLE") != null ?
                                "Send Result : " + (string)rds3Result.Element("VEHICLE").Attribute("vin") + ", " + (string)rds3Result.Element("VEHICLE").Element("VISIT").Attribute("tester") :
                                "Request Results : " + (string)rds3Result.Element("REQUEST").Attribute("vin")
                });

                return rds3Result;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An exception has occured parsing the data. See inner exception for details", e);
            }
        }

        #endregion

        public override string Describe()
        {
            return "RDS3 Parser";
        }

        public event EventHandler<LogEventArgs> LogEvent;

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });
        }

        public void ParseStringToXml(object sender, ParseStringToXmlEventArgs e)
        {
            if (e.Data.Contains("<VEHICLE rds=\"3.0\"") | e.Data.Contains("<REQUEST rds=\"3.0\""))
            {
                try
                {
                    e.Xml = Parse(e.Data);
                }
                catch (ApplicationException)
                {
                    e.Xml = null;
                }
            }
        }
    }
}

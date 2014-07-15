using System;
using System.Linq;
using System.Text;

namespace Actemium.Stratus.MailboxPlugin.Parse
{
    public class ParserFactory
    {
        #region Public Static Methods

        public static IParser GetParser(byte[] data)
        {
            return GetParser(Encoding.ASCII.GetString(data));
        }

        public static IParser GetParser(string data)
        {
            if (data.Contains("<VEHICLE rds=\"3.0\"") | data.Contains("<REQUEST rds=\"3.0\""))
            {
                return new Rds3Parser();
            }
            else if (data.Contains("STATION:"))
            {
                return new Rds1Parser();
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}

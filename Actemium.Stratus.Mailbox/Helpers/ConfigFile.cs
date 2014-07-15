using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Actemium.Stratus.MailboxPlugin.Helpers
{
    public static class ConfigFile<TS,TK>
    {
        public static bool Updated { get; set; }

        private static DateTime lastModificationDate = DateTime.MinValue;
        private static readonly Dictionary<TS, Dictionary<TK, object>> configuration = new Dictionary<TS, Dictionary<TK, object>>();

        #region Public Static Methods

        public static Dictionary<TS, Dictionary<TK, object>> Parse(string fileName, string rootElement)
        {
            var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (File.Exists(directory + "\\" + fileName))
            {
                var modificationDate = File.GetLastWriteTime(directory + "\\" + fileName);
                
                if (lastModificationDate != modificationDate)
                {
                    lastModificationDate = modificationDate;
                    Updated = true;

                    var config = XDocument.Load(directory + "\\" + fileName);
                    foreach (var category in Enum.GetNames(typeof(TS)))
                    {
                        var configSection = (TS)Enum.Parse(typeof(TS), category);

                        configuration[configSection] = new Dictionary<TK, object>();

                        var categoryElement = config.Element(rootElement).Element(category);

                        if (categoryElement != null)
                        {
                            foreach (var key in categoryElement.Elements())
                            {
                                try
                                {
                                    configuration[configSection][(TK)Enum.Parse(typeof(TK), key.Name.LocalName)] = key.Value;

                                }
                                catch (InvalidCastException)
                                {
                                    //TODO: Log that the xml contains an unknown element
                                }
                            }
                        }
                    }
                }
                else
                {
                    Updated = false;
                }
            }

            return configuration;
        }

        #endregion
    }
}

using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace Actemium.Stratus.MailboxPlugin.Write
{
    public class Rds3Writer : ShopFloorResultsExtensionBase, IWriter
    {
        private string basePath;
        private string duplicateDirectory;

        private readonly string fileExtension = ".rds";
        private readonly List<XDocument> xmlQueue = new List<XDocument>();

        public void XDocumentAvailableHandler(object sender, XDocumentAvailableEventArgs e)
        {

            if (e.Data.Element("VEHICLE") != null)
            {
                //If we can't save the file
                if (!Save(e.Data))
                    //queue the file to try later
                    xmlQueue.Add(e.Data);
            }
        }

        /// <summary>
        /// Saves an RDS3 file to disk, either in the Good directory or if already received added to the duplicate directory
        /// </summary>
        /// <param name="xDocument">The RDS3 file to create</param>
        /// <returns>True if the RDS3 file was saved, False if it was not possible to save</returns>
        private bool Save(XDocument xDocument)
        {
            ////Check the output directory and exit if they don't exist
            //if (basePath == null | goodDirectory == null | !Directory.Exists(basePath + "\\" + goodDirectory))
            //    return false;

            //Create a suitable filename for the result packet
            var fileName = Regex.Replace(((string)xDocument.Element("VEHICLE").Element("VISIT").Attribute("tester")) + "_" +
                (string)xDocument.Element("VEHICLE").Attribute("vin") + "_" +
                ((DateTime)xDocument.Element("VEHICLE").Element("VISIT").Attribute("start")).ToString("dd-MM-yyyy_HH-mm-ss"), @"[^\w\.@-]", "_", RegexOptions.None);

            ////If the result packet doesn't exist in the good directory 
            //if (!File.Exists(basePath + "\\" + goodDirectory + "\\" + fileName + fileExtension))
            //{
            //    //It must be the first time it has arrived
            //    xDocument.Save(basePath + "\\" + goodDirectory + "\\" + fileName + fileExtension);
            //    return true;
            //}

            //otherwise it must be a duplicate so get a unique filename in the duplicate directory
            fileName = GetUniqueFilenameInDuplicateDirectory(fileName);

            //If we don\t have a valid filename the paths are invalid so exit
            if (fileName == null)
                return false;

            //Save the file to the duplicate directory
            xDocument.Save(basePath + "\\" + duplicateDirectory + "\\" + fileName + fileExtension);
            return true;

        }

        private string GetUniqueFilenameInDuplicateDirectory(string fileName)
        {
            //if the directory does not exist return null
            if (basePath == null | duplicateDirectory == null | !Directory.Exists(basePath + "\\" + duplicateDirectory))
                return null;

            //While the suggested file exists
            var dupCount = 0;
            var newFileName = fileName;

            while (File.Exists(basePath + "\\" + duplicateDirectory + "\\" + newFileName + fileExtension))
            {
                //make a new version of the filename
                newFileName = fileName + "-" + ++dupCount;
            }

            return newFileName;
        }

        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.OutputDirectories)
            {
                basePath = (string)e.Configuration[ConfigKey.BaseDirectory];
                duplicateDirectory = (string)e.Configuration[ConfigKey.Duplicate];

                foreach (var xml in xmlQueue)
                {
                    Save(xml);
                }
            }
        }

        public override string Describe()
        {
            return "RDS3 XML Writer";
        }

        public event EventHandler<LogEventArgs> LogEvent;

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }
    }
}

using Ninject.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WhoSol.Contracts;
using WhoSol.XMLDBPlugin.Properties;

namespace WhoSol.XMLDBPlugin
{
    public sealed class XMLDBUnitOfWork : IUnitOfWork
    {
        private XDocument db;
        private ILogger logger;

        public XMLDBUnitOfWork(ILogger logger)
        {
            this.logger = logger;
        }
        public void Commit()
        {
            db.Save(ConnectionString);
        }

        public int ExecuteSp(string p, params SqlParameter[] zoneParam)
        {
            throw new NotImplementedException(Resources.ErrorExecuteSP);
        }

        public bool Exists
        {
            get;
            private set;
        }

        public string ConnectionString
        {
            get;
            private set;
        }

        public void Initialise(params object[] args)
        {

            //Expected 3 arguments, Root Directory, Database Filename & Database schema as an XDocument 
            if (args.Length == 3 && args[0] is string && args[1] is string && (args[2] == null || args[2] is XDocument))
            {

                string xmlDbPath = args[0] as string + "XMLDB\\";
                string xmlDbFileName = args[1] as string;
                XDocument schema = args[2] as XDocument;

                ConnectionString = xmlDbPath + xmlDbFileName;

                if (!Directory.Exists(xmlDbPath))
                {
                    Directory.CreateDirectory(xmlDbPath);
                }

                if (File.Exists(ConnectionString))
                {
                    db = XDocument.Load(ConnectionString);

                    if (!ValidateSchema(db, schema))
                    {
                        logger.Error("Incorrect Schema");
                        db = schema;
                        db.Save(ConnectionString);
                    }
                }
                else
                {
                    db = schema;
                    db.Save(ConnectionString);
                }
                Exists = true;
            }
        }

        public void Dispose()
        {
            db.Save(ConnectionString);
        }

        public object Store
        {
            get { return db; }
            set
            {
                if (value is XDocument)
                {
                    db = value as XDocument;
                }
                else
                {
                    throw new ArgumentException("Store is not of type XDocument");
                }
            }
        }

        private bool ValidateSchema(XDocument fromFile, XDocument schema)
        {
            var fileElements = fromFile.Descendants().Where(e => e.Attribute("Entity") == null).Select(e => e.Name.LocalName).Distinct().ToArray();
            var schemaElements = schema.Descendants().Select(e => e.Name.LocalName).ToArray();

            return fileElements.SequenceEqual(schemaElements) ? true : false;
        }
    }
}

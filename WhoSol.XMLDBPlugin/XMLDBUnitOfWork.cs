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
            if (args.Length == 2 && args[0] is string && (args[1] == null || args[1] is XDocument))
            {
                ConnectionString = args[0] as string;
                if (File.Exists(ConnectionString))
                {
                    db = XDocument.Load(ConnectionString);

                    if (!ValidateSchema(db, args[1] as XDocument))
                    {
                        logger.Error("Incorrect Schema");
                        db = args[1] as XDocument;
                        db.Save(ConnectionString);
                    }
                }
                else
                {
                    db = args[1] as XDocument;
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

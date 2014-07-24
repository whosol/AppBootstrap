using System;
using System.Linq;
using Actemium.Stratus.Contracts;
using System.Data.SqlClient;
using Actemium.Stratus.Contracts.Enums;
using Ninject.Extensions.Logging;
using Actemium.Stratus.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin
{
    public class RepositoryPlugin : BasePlugin
    {
        private readonly IUnitOfWork uow;

        public override void Start()
        {
            try
            {
                uow.Initialise();
                logger.Info("Database connected and initialised");
            }
            catch (SqlException ex)
            {
                uow.Dispose();

                var rex = new RepositoryException("Cannot open database. Please check the connection string", ErrorLevel.Fatal, ex);
                logger.FatalException(rex.Message, rex);
                throw rex;
            }
        }

        public override void Stop()
        {
            logger.Info("Stopping Repository Plugin");
            uow.Dispose();
            logger.Info("Stopped Repository Plugin");
        }

        public override string Description
        {
            get { return "Repository Plugin for accessing Stratus 7 Database"; }
        }

        public RepositoryPlugin(ILogger logger, IConfiguration configuration, IUnitOfWork uow)
            : base(logger, configuration)
        {
            this.uow = uow;
        }
    }
}

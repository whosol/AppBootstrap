using System;
using System.Linq;
using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.RepositoryPlugin
{
    public class RepositoryPlugin : PluginBase
    {
        private readonly IUnitOfWork uow;

        public override void Start()
        {
            uow.Initialise();
        }

        public override void Stop()
        {
            uow.Dispose();
        }

        public override string Description
        {
            get { return "Repository Plugin for accessing Stratus 7 Database"; }
        }

        public RepositoryPlugin(IConfiguration configuration, IUnitOfWork uow)
            : base(configuration)
        {
            this.uow = uow;
        }
    }
}

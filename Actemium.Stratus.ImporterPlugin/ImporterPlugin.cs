using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.ImporterPlugin
{
    public class ImporterPlugin : BasePlugin
    {
        public override string Description
        {
            get { return "RDS3 File format importer"; }
        }

        public ImporterPlugin(ILogger logger,IConfiguration configuration)
            : base(logger, configuration)
        {
        }
    }
}

using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.ImporterPlugin
{
    public class ImporterPlugin : PluginBase
    {
        public override string Description
        {
            get { return "RDS3 File format importer"; }
        }

        public ImporterPlugin(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

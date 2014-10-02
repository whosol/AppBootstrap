using Ninject.Extensions.Logging;
using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using WhoSol.XMLDBPlugin.Properties;

namespace WhoSol.XMLDBPlugin
{
    public class XMLDBPlugin : BasePlugin
    {
        public XMLDBPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            Autostart = false;
        }

        public override string Description
        {
            get
            {
                return Resources.PluginDescription;
            }
        }
    }
}

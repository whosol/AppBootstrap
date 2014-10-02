using Ninject.Extensions.Logging;
using WhoSol.Contracts;
using WhoSol.Contracts.Base;

namespace WhoSol.EntityFrameworkPlugin
{
    public class EntityFrameworkPlugin: BasePlugin
    {
        public EntityFrameworkPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            Autostart = false;
        }

        public override string Description
        {
            get { return "Entity Framework Plugin"; }
        }
    }
}

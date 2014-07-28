using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using Ninject.Extensions.Logging;
using System;

namespace WhoSol.WebApiPlugin
{
    public class WebApiPlugin : BasePlugin
    {
        public override string Description
        {
            get { return "WebApi plugin for service controller"; }
        }

        public WebApiPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
        }
    }
}

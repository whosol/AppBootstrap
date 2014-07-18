using Actemium.Stratus.Contracts;
using Ninject.Extensions.Logging;
using System;

namespace Actemium.Stratus.WebApi
{
    public class WebApiPlugin : PluginBase
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

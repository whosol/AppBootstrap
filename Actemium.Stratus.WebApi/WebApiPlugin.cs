using Actemium.Stratus.Contracts;
using System;

namespace Actemium.Stratus.WebApi
{
    public class WebApiPlugin : PluginBase
    {
        public override string Description
        {
            get { return "WebApi plugin for service controller"; }
        }

        public WebApiPlugin(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}

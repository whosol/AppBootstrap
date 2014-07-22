using Actemium.Stratus.Contracts;
using Actemium.Stratus.ServiceController.Controllers.Dto;
using System.Web.Http;
using System.Linq;

namespace Actemium.Stratus.ServiceController.Controllers
{
    public class PluginsController : ApiController
    {
        private readonly IPlugin[] plugins;

        public PluginsController(IPlugin[] plugins)
        {
            this.plugins = plugins;
        }

        public PluginsDto Get()
        {
            return new PluginsDto
            {
                Plugins = (from plugin in plugins
                           select new PluginDto
                           {
                               Name = plugin.Name,
                               Description = plugin.Description,
                               Version = plugin.Version,
                               FileVersion = plugin.FileVersion,
                               Location = plugin.Location
                           }).ToArray()
            };
        }
    }
}

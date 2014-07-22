using Actemium.Stratus.Contracts;
using Actemium.Stratus.ServiceController.Controllers.Dto;
using System.Web.Http;
using System.Linq;

namespace Actemium.Stratus.ServiceController.Controllers
{
    public class ServiceStatusController : ApiController
    {
        private readonly IPlugin[] plugins;

        public ServiceStatusController(IPlugin[] plugins)
        {
            this.plugins = plugins;
        }

        public ServiceStatusDto Get()
        {
            return new ServiceStatusDto
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

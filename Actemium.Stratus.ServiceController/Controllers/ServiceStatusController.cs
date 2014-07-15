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

        public ServiceStatus Get() 
        {
            return new ServiceStatus 
            {
                Plugins = (from plugin in plugins select new Plugin{Name = plugin.Name, Description = plugin.Description}).ToArray()
            };
        }
    }
}

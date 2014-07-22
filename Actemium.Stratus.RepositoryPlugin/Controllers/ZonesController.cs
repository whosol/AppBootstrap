using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ZonesController : BaseController
    {
        public ZonesController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public ZonesDto Get()
        {
            return new ZonesDto
            {
                Zones = uow.Zones.FindAll()
                .OrderBy(z => z.Name)
                .Select(z => new ZoneDto 
                {
                    Id = z.Id,
                    Name = z.Name
                })
            };
        }
    }
}

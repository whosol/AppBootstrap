using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ZonesController : BaseController
    {
        public ZonesController(IUnitOfWork uow)
            : base(uow)
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

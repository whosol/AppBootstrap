using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ZonesController : StratusBaseController<Zone, ZoneDto, ZonesDto>
    {
        public ZonesController(IStratusUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override ZoneDto CreateDto(Zone dataObject)
        {
            return new ZoneDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override ZonesDto GetAll()
        {
            return new ZonesDto
            {
                Zones = uow.Zones.FindAll()
                    .OrderBy(z => z.Name)
                    .ToList()
                    .Select(z => CreateDto(z))
            };
        }

        public override ZoneDto GetById(int id)
        {
            var zone = uow.Zones.FindById(id);
            return zone != null ? CreateDto(zone) : null;
        }
    }
}

using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;
using Actemium.Stratus.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class LocationsController : BaseController<Location, LocationDto, LocationsDto>
    {
        public LocationsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override LocationDto CreateDto(Location dataObject)
        {
            return new LocationDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override LocationsDto Get()
        {
            return new LocationsDto
            {
                Locations = uow.Locations.FindAll()
                    .OrderBy(l => l.Name)
                    .ToList()
                    .Select(l => CreateDto(l))
            };
        }

        public override LocationDto Get(int id)
        {
            var location = uow.Locations.FindById(id);
            return location != null ? CreateDto(location) : null;
        }
    }
}

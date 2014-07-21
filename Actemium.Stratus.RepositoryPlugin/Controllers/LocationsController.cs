using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class LocationsController : BaseController
    {
        public LocationsController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public LocationsDto Get()
        {
            return new LocationsDto
            {
                Locations = uow.Locations.FindAll()
                .OrderBy(l => l.Name)
                .Select(l => new LocationDto
                {
                    Id = l.Id,
                    Name = l.Name
                })
            };
        }
    }
}

using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;
using Actemium.Stratus.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class PlantsController : BaseController<Plant, PlantDto, PlantsDto>
    {
        public PlantsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override PlantDto CreateDto(Plant dataObject)
        {
            return new PlantDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override PlantsDto Get()
        {
            return new PlantsDto
            {
                Plants = uow.Plants.FindAll()
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => CreateDto(p))
            };
        }

        public override PlantDto Get(int id)
        {
            var plant = uow.Plants.FindById(id);
            return plant != null ? CreateDto(plant) : null;
        }
    }
}

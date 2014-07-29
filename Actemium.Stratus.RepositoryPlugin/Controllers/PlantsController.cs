using System;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class PlantsController : StratusBaseController<Plant, PlantDto, PlantsDto>
    {
        public PlantsController(IStratusUnitOfWork uow, ILogger logger)
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

        public override PlantsDto GetAll()
        {
            return new PlantsDto
            {
                Plants = uow.Plants.FindAll()
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => CreateDto(p))
            };
        }

        public override PlantDto GetById(int id)
        {
            var plant = uow.Plants.FindById(id);
            return plant != null ? CreateDto(plant) : null;
        }
    }
}

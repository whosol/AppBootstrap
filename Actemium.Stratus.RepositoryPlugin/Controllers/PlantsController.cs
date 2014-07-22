using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class PlantsController : BaseController
    {
        public PlantsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public PlantsDto Get()
        {
            return new PlantsDto
            {
                Plants = uow.Plants.FindAll()
                .OrderBy(p => p.Name)
                .Select(p => new PlantDto 
                {
                    Id = p.Id,
                    Name = p.Name
                })
            };
        }
    }
}

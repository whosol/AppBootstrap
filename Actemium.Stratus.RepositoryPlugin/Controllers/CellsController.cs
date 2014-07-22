using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class CellsController : BaseController
    {
        public CellsController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }
        public CellsDto Get()
        {
            return new CellsDto
            {
                Cells = uow.Cells.FindAll()
                .OrderBy(c => c.Name)
                .Select(c => new CellDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
            };
        }
    }
}

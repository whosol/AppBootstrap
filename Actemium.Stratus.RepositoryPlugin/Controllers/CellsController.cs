using System;
using System.Linq;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class CellsController : StratusBaseController<Cell, CellDto, CellsDto>
    {
        public CellsController(IStratusUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }
        public override CellDto CreateDto(Cell dataObject)
        {
            return new CellDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override CellsDto Get()
        {
            return new CellsDto
            {
                Cells = uow.Cells.FindAll()
                    .OrderBy(c => c.Name)
                    .ToList()
                    .Select(c => CreateDto(c))
            };
        }

        public override CellDto Get(int id)
        {
            var cell = uow.Cells.FindById(id);
            return cell != null ? CreateDto(cell) : null;
        }
    }
}

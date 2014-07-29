using System;
using System.Linq;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Web.Http;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    [RoutePrefix("api/cells")]
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

        public CellsDto Get(int? productTypes = null, int? products = null,
            int? sequences = null, int? plants = null, int? processes = null, int? locations = null,
            int? zones = null, int? cells = null)
        {
            var query = uow.Cells.FindAll();

            return null;
        }

        [Route("all")]
        public override CellsDto GetAll()
        {
            return new CellsDto
            {
                Cells = uow.Cells.FindAll()
                    .OrderBy(c => c.Name)
                    .ToList()
                    .Select(c => CreateDto(c))
            };
        }

        public override CellDto GetById(int id)
        {
            var cell = uow.Cells.FindById(id);
            return cell != null ? CreateDto(cell) : null;
        }
    }
}

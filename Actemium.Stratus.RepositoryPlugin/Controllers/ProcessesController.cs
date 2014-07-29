using System;
using System.Linq;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Base;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProcessesController : StratusBaseController<Process, ProcessDto, ProcessesDto>
    {
        public ProcessesController(IStratusUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override ProcessDto CreateDto(Process dataObject)
        {
            return new ProcessDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override ProcessesDto GetAll()
        {
            return new ProcessesDto
            {
                Processes = uow.Processes.FindAll()
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => CreateDto(p))
            };
        }

        public override ProcessDto GetById(int id)
        {
            var process = uow.Processes.FindById(id);
            return process != null ? CreateDto(process) : null;
        }
    }
}

using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProcessesController : BaseController<Process, ProcessDto, ProcessesDto>
    {
        public ProcessesController(IUnitOfWork uow, ILogger logger)
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

        public override ProcessesDto Get()
        {
            return new ProcessesDto
            {
                Processes = uow.Processes.FindAll()
                .OrderBy(p => p.Name)
                .ToList()
                .Select(p => CreateDto(p))
            };
        }

        public override ProcessDto Get(int id)
        {
            var process = uow.Processes.FindById(id);
            return process != null ? CreateDto(process) : null;
        }
    }
}

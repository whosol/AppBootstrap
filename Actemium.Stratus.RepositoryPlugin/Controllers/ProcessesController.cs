using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class ProcessesController : BaseController
    {
        public ProcessesController(IUnitOfWork uow)
            :base(uow)
        {

        }

        public ProcessesDto Get()
        {
            return new ProcessesDto
            {
                Processes = uow.Processes.FindAll()
                .OrderBy(p => p.Name)
                .Select(p => new ProcessDto 
                {
                    Id = p.Id,
                    Name = p.Name
                })
            };
        }
    }
}

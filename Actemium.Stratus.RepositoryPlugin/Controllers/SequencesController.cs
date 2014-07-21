using Actemium.Stratus.Contracts;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using System.Linq;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class SequencesController : BaseController
    {
        public SequencesController(IUnitOfWork uow)
            : base(uow)
        {

        }

        public SequencesDto Get()
        {
            return new SequencesDto
            {
                Sequences = uow.Sequences.FindAll()
                .OrderBy(s => s.Name)
                .Select(s => new SequenceDto 
                {
                    Id =s.Id,
                    Name = s.Name
                })
            };
        }
    }
}

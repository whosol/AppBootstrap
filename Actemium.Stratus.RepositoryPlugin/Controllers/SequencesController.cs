using System;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using Actemium.Stratus.RepositoryPlugin.Controllers.Dto;
using Ninject.Extensions.Logging;
using System.Linq;
using Actemium.Stratus.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public class SequencesController : BaseController<Sequence, SequenceDto, SequencesDto>
    {
        public SequencesController(IUnitOfWork uow, ILogger logger)
            : base(uow, logger)
        {

        }

        public override SequenceDto CreateDto(Sequence dataObject)
        {
            return new SequenceDto
            {
                Id = dataObject.Id,
                Name = dataObject.Name
            };
        }

        public override SequencesDto Get()
        {
            return new SequencesDto
            {
                Sequences = uow.Sequences.FindAll()
                    .OrderBy(s => s.Name)
                    .ToList()
                    .Select(s => CreateDto(s))
            };
        }

        public override SequenceDto Get(int id)
        {
            var sequence = uow.Sequences.FindById(id);
            return sequence != null ? CreateDto(sequence) : null;
        }


    }
}

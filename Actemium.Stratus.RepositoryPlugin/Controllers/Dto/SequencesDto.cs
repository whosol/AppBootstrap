﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class SequencesDto : BaseCollectionDto
    {
        public SequencesDto()
        {
            controllerName = "sequences";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<SequenceDto> Sequences { get; set; }

    }
}
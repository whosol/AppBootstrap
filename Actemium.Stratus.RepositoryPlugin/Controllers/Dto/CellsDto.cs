using Actemium.Stratus.Contracts.Base;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class CellsDto : BaseCollectionDto
    {
        public CellsDto()
        {
            controllerName = "cells";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<CellDto> Cells { get; set; }

    }
}
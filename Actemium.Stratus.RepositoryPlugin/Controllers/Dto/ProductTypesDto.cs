using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ProductTypesDto : BaseCollectionDto
    {
        public ProductTypesDto()
        {
            controllerName = "producttypes";
        }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProductTypeDto> ProductTypes { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ProductTypesDto : WrapperBaseDto
    {
        public ProductTypesDto()
        {
            controllerName = "producttypes";
        }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProductTypeDto> ProductTypes { get; set; }

    }
}
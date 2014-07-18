using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class ProductTypesDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProductTypeDto> ProductTypes { get; set; }

    }
}
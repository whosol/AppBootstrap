using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ProductsDto : BaseCollectionDto
    {
        public ProductsDto()
        {
            controllerName = "products";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ProductDto> Products { get; set; }

    }
}
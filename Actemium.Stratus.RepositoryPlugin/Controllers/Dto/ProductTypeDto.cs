using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DebuggerDisplay("{Name}")]
    [DataContract(Name = "ProductType")]
    public class ProductTypeDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<ProductDto> Products { get; set; }

        public bool ShouldSerializeProducts()
        {
            return null != this.Products && this.Products.Any();
        }
    }
}
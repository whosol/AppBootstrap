using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "Product")]
    public class ProductDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string ProductUniqueId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public ProductTypeDto ProductType { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<CompanyDto> Companies { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 5)]
        public IEnumerable<ResultDto> Results { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 6)]
        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
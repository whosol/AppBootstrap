using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "Company")]
    public class CompanyDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<PlantDto> Plants { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<ProductDto> Products { get; set; }

        public bool ShouldSerializePlants()
        {
            return null != this.Plants && this.Plants.Any();
        }
        public bool ShouldSerializeProducts()
        {
            return null != this.Products && this.Products.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}
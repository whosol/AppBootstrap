using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "Plant")]
    public class PlantDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
        public IEnumerable<TesterDto> Testers { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
        public IEnumerable<CompanyDto> Companies { get; set; }

        public bool ShouldSerializeTesters()
        {
            return null != this.Testers && this.Testers.Any();
        }

        public bool ShouldSerializeCompanies()
        {
            return null != this.Companies && this.Companies.Any();
        }

        public IEnumerable<VisitDto> Visits { get; set; }
    }
}

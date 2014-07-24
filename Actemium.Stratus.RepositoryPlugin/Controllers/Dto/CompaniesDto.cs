using Actemium.Stratus.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class CompaniesDto : BaseCollectionDto
    {
        public CompaniesDto()
        {
            controllerName = "companies";
        }
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<CompanyDto> Companies { get; set; }

    }
}
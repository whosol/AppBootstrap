using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class ZonesDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ZoneDto> Zones { get; set; }
    }
}
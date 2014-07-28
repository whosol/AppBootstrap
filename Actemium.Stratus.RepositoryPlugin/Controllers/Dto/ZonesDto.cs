using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WhoSol.Contracts.Base;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ZonesDto : BaseCollectionDto
    {
        public ZonesDto()
        {
            controllerName = "zones";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ZoneDto> Zones { get; set; }
    }
}
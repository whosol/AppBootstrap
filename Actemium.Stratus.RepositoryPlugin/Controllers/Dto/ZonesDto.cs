using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ZonesDto : WrapperBaseDto
    {
        public ZonesDto()
        {
            controllerName = "zones";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<ZoneDto> Zones { get; set; }
    }
}
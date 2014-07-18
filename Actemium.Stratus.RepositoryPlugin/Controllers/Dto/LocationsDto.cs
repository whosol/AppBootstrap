﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract(Name = "StratusApi")]
    public class LocationsDto
    {
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<LocationDto> Locations { get; set; }

    }
}
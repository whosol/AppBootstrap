using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class LocationsDto : WrapperBaseDto
    {
        public LocationsDto()
        {
            controllerName = "locations";
        }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public IEnumerable<LocationDto> Locations { get; set; }

    }
}
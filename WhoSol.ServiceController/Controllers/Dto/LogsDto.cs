using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace WhoSol.ServiceController.Controllers.Dto
{
    [DataContract(Namespace="")]
    public class LogsDto
    {
        [DataMember]
        public IEnumerable<LogDto> Logs;
    }
}

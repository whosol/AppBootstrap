using System;
using System.Runtime.Serialization;
using WhoSol.ServiceController.Enums;

namespace WhoSol.ServiceController.Controllers.Dto
{
    [DataContract(Namespace = "")]
    public class LogDto
    {
        [DataMember(Order = 1)]
        public string Timestamp { get; set; }
        [DataMember(Order = 2)]
        public string LogLevel { get; set; }
        [DataMember(Order = 3)]
        public string Class { get; set; }
        [DataMember(Order = 4)]
        public int Thread { get; set; }
        [DataMember(Order = 5)]
        public string Message { get; set; }
    }
}

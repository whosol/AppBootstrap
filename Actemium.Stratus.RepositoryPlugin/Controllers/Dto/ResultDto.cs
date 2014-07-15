using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ResultDto
    {
        [DataMember]
        public string ProductUniqueId { get; set; }
        [DataMember]
        public string SequenceName { get; set; }
        [DataMember]
        public string ResultName { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string UpperLimit { get; set; }
        [DataMember]
        public string LowerLimit { get; set; }
        [DataMember]
        public string Units { get; set; }
    }
}

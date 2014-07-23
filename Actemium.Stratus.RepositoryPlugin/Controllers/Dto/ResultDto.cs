using System.Runtime.Serialization;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class ResultDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double RelativeTime { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public bool? IsFixedLimit { get; set; }
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
        [DataMember]
        public bool IsChild { get; set; }
        [DataMember]
        public bool IsParent { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}

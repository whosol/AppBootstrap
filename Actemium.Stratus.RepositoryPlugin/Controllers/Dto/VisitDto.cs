using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Actemium.Stratus.RepositoryPlugin.Controllers.Dto
{
    [DataContract]
    public class VisitDto
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string ProductUniqueId { get; set; }

        [DataMember(Order = 3)]
        public DateTime StartTime { get; set; }

        [DataMember(Order = 4)]
        public DateTime EndTime { get; set; }

        [DataMember(Order = 5)]
        public int Duration { get; set; }

        [DataMember(Order = 6)]
        public string Status { get; set; }

        /// <summary>
        /// Serialised only for the XML return type. Parses VisitXml to create an XmlElement to add to XML DTO
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 7)]
        [JsonIgnore]
        public XmlElement Xml
        {
            get
            {
                if (VisitXml != null)
                {
                    var doc = new XmlDocument();
                    doc.InnerXml = this.VisitXml;
                    //This is essential to ensure that the VisitXml is not serialised
                    //when the return DTO is to be XML. This will not be called if the
                    //DTO is JSON due to the JsonIgnore attribute on the property
                    VisitXml = null;
                    return doc.DocumentElement;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Serialized only for the JSON return type. Set order as 999 to ensure this is the very last property to be 
        /// serialized. 
        /// </summary>
        [DataMember(EmitDefaultValue = false, Order = 999)]
        public string VisitXml { get; set; }

    }
}
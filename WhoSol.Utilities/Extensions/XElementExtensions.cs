using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WhoSol.Utilities.Extensions
{
    public static class XElementExtensions
    {
        public static XElement ToXElement<T>(this object obj)
        {
            using (var memoryStream = new MemoryStream())
            using (TextWriter streamWriter = new StreamWriter(memoryStream))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(streamWriter, obj, ns);
                return XElement.Parse(Encoding.ASCII.GetString(memoryStream.ToArray()));
            }

        }

        public static T FromXElement<T>(this XElement xElement)
        {
            using (var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xElement.ToString())))
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}

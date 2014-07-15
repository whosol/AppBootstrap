using Actemium.Stratus.Contracts.Enums;
using System.Xml.Linq;

namespace Actemium.Stratus.Contracts
{
    public interface IImporter
    {
        PersistStatus PersistResult(XDocument visitXml);
    }
}

using WhoSol.Contracts.Enums;
using System.Xml.Linq;

namespace WhoSol.Contracts
{
    public interface IImporter
    {
        PersistStatus PersistResult(XDocument visitXml);
    }
}

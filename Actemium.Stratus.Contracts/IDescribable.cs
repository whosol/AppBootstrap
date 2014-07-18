
namespace Actemium.Stratus.Contracts
{
    public interface IDescribable
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }
        string Location { get; }
    }
}

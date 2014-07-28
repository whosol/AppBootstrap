
namespace WhoSol.Contracts
{
    public interface IDescribable
    {
        string Name { get; }
        string Description { get; }
        string Version { get; }
        string FileVersion { get; }

        string Location { get; }
    }
}

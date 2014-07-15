
namespace Actemium.Stratus.Contracts
{
    public interface IPlugin : IDescribable
    {
        void Start();

        void Stop();
    }
}

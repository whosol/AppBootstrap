
namespace WhoSol.Contracts
{
    public interface IPlugin : IDescribable
    {
        void Start();

        void Stop();
    }
}

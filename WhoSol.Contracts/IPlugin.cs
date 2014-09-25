
namespace WhoSol.Contracts
{
    public interface IPlugin : IDescribable
    {
        void Start(params object[] args);

        void Stop();

        bool Autostart { get; }
    }
}

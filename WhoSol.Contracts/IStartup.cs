using Owin;

namespace WhoSol.Contracts
{
    public interface IStartup
    {
        void Configuration(IAppBuilder app);
    }
}

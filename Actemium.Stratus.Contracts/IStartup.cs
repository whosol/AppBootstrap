using Owin;

namespace Actemium.Stratus.Contracts
{
    public interface IStartup
    {
        void Configuration(IAppBuilder app);
    }
}

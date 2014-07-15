
namespace Actemium.Stratus.Contracts
{
    public abstract class PluginBase : IPlugin
    {
        protected readonly IConfiguration configuration;

        public PluginBase(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual string Name
        {
            get { return this.GetType().Name; }
        }

        public abstract string Description { get; }
    }
}

using System;
using Ninject.Extensions.Logging;
using System.Diagnostics;

namespace Actemium.Stratus.Contracts
{
    public abstract class PluginBase : IPlugin
    {
        protected readonly IConfiguration configuration;
        protected readonly ILogger logger;

        public PluginBase(ILogger logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
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

        public string Version
        {
            get
            {
                return this.GetType().Assembly.GetName().Version.ToString();
            }
        }

        public string FileVersion
        {
            get
            {
                return FileVersionInfo.GetVersionInfo(this.GetType().Assembly.Location).FileVersion;
            }
        }

        public string Location
        {
            get
            {
                return this.GetType().Assembly.Location;
            }
        }
    }
}

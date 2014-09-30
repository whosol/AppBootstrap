using System;
using Ninject.Extensions.Logging;
using System.Diagnostics;

namespace WhoSol.Contracts.Base
{
    public abstract class BasePlugin : IPlugin
    {
        protected readonly IConfiguration configuration;
        protected readonly ILogger logger;

        public BasePlugin(ILogger logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.logger = logger;
            Autostart = true;
        }

        public virtual void Start(params object[] args)
        {
            logger.Info("Started {0} Plugin", this.ToString());
        }

        public virtual void Stop()
        {
            logger.Info("Stopped {0} Plugin", this.ToString());
        }

        public bool Autostart { get; protected set; }

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

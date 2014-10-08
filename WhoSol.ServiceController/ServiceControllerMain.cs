using Ninject.Extensions.Logging;
using System;
using WhoSol.Contracts;
using WhoSol.Contracts.Enums;
using WhoSol.Contracts.Exceptions;

namespace WhoSol.ServiceController
{
    public class ServiceControllerMain : IController
    {
        private readonly IPlugin[] plugins;
        private readonly ILogger logger;

        public ServiceControllerMain(ILogger logger, IPlugin[] plugins)
        {
            this.logger = logger;
            this.plugins = plugins;
        }

        public void Stop()
        {
            foreach (var plugin in plugins)
            {
                if (plugin.Autostart)
                {
                    plugin.Stop();                    
                }
            }
        }

        public void Start()
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    if (plugin.Autostart)
                    {
                        plugin.Start();
                    }
                }
                catch (StratusException ex)
                {
                    switch (ex.ErrorLevel)
                    {
                        case ErrorLevel.Fatal:
                            throw ex;
                        case ErrorLevel.Recoverable:
                        case ErrorLevel.Warning:
                        case ErrorLevel.Information:
                            //TODO: Log to Error log
                            break;
                        default:
                            break;
                    }

                }
                catch(Exception ex)
                {
                    logger.ErrorException(string.Format("Failed to start plugin {0}", plugin), ex);
                    plugin.Stop();
                }
            }
        }
    }
}

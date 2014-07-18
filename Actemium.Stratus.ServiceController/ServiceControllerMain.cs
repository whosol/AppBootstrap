using Actemium.Stratus.Contracts;
using Actemium.Stratus.Contracts.Enums;
using Actemium.Stratus.Contracts.Exceptions;
using System;
using System.Diagnostics;

namespace Actemium.Stratus.ServiceController
{
    public class ServiceControllerMain : IController
    {
        private readonly IPlugin[] plugins;

        public ServiceControllerMain(IPlugin[] plugins)
        {
            this.plugins = plugins;
        }

        public void Stop()
        {
            foreach (var plugin in plugins)
            {
                plugin.Stop();
            }
        }

        public void Start()
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    plugin.Start();
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
            }
        }
    }
}

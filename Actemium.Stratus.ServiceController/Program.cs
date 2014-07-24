using System.IO;
using Actemium.Stratus.Contracts;
using Actemium.Stratus.ServiceController.Properties;
using Ninject;
using Topshelf;
using System.Reflection;
using System;
using Actemium.Stratus.Contracts.Exceptions;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Extensions.Logging;
using log4net.Config;

namespace Actemium.Stratus.ServiceController
{
    class Program
    {
        private static ILogger logger;
        private static IKernel kernel;
        private static IConfiguration configuration;

        private static void Main(string[] args)
        {
            CreateKernel();

            InitialiseConfiguration();

            LoadAssemblies("ThirdPartyDirectory", "Actemium.Stratus.ThirdParty.dll");

            LoadAssemblies("PluginDirectory", "*.Stratus.*Plugin.dll");

            StartService();
        }
        private static void InitialiseConfiguration()
        {
            configuration = kernel.Get<IConfiguration>();

            configuration.Set("RootDirectory", AssemblyDirectory);
            configuration.Set("PluginDirectory", AssemblyDirectory + "Plugins\\");
            configuration.Set("ThirdPartyDirectory", AssemblyDirectory + "ThirdParty\\");
        }

        private static void CreateKernel()
        {
            BasicConfigurator.Configure();
            // XmlConfigurator.Configure();
            kernel = new StandardKernel(new ServiceControllerModule(), new Log4NetModule());

            GetLogger();

            logger.Debug("Ninject Kernel created");
        }

        private static void StartService()
        {
            HostFactory.Run(x =>
            {
                x.Service<IController>(s =>
                {
                    try
                    {
                        s.ConstructUsing(name => kernel.Get<IController>());
                        s.WhenStarted(controller => controller.Start());
                        s.WhenStopped(controller => controller.Stop());
                    }
                    catch (StratusException ex)
                    {
                        logger.FatalException(ex.Message, ex);
                        return;
                    }
                });
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription(Resources.ServiceDescription);
                x.SetDisplayName(Resources.ServiceDisplayName);
                x.SetServiceName(Resources.ServiceName);

            });
        }

        private static void LoadAssemblies(string directoryKey, string filter)
        {
            string assemblySearchFilter = string.Empty;
            if (Directory.Exists(configuration.Get<string>(directoryKey)))
            {
                assemblySearchFilter = string.Format("{0}{1}", configuration.Get<string>(directoryKey), filter);
            }
            else
            {
                assemblySearchFilter = string.Format("{0}{1}", configuration.Get<string>("RootDirectory"), filter);
            }

            kernel.Load(assemblySearchFilter);
            logger.Debug(string.Format("Loaded assemblies from {0}", assemblySearchFilter));
        }

        private static void GetLogger()
        {
            var loggerFactory = kernel.Get<ILoggerFactory>();
            logger = loggerFactory.GetCurrentClassLogger();
        }

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path) + "\\";
            }
        }
    }
}

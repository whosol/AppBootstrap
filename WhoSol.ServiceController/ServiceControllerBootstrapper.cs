using log4net.Config;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net;
using System;
using System.IO;
using System.Reflection;
using Topshelf;
using WhoSol.Contracts;
using WhoSol.Contracts.Exceptions;

namespace WhoSol.ServiceController
{
    public class ServiceControllerBootstrapper : IBootstrapper
    {
        private static ILogger logger;
        private static IKernel kernel;
        private static IConfiguration configuration;
        private readonly string serviceName;
        private readonly string serviceDisplayName;
        private readonly string serviceDescription;

        public ServiceControllerBootstrapper(string serviceName, string serviceDisplayName, string serviceDescription)
        {
            this.serviceName = serviceName;
            this.serviceDisplayName = serviceDisplayName;
            this.serviceDescription = serviceDescription;
        }

        public void Start()
        {
            CreateKernel();

            InitialiseConfiguration();

            LoadAssemblies("ThirdPartyDirectory", "*ThirdParty.dll");

            LoadAssemblies("PluginDirectory", "*Plugin.dll");

            StartService();
        }

        public void Stop()
        {

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

            logger.Info("Ninject Kernel created");
        }

        private void StartService()
        {
            HostFactory.Run(x =>
            {
                x.Service<IController>(s =>
                {
                    s.ConstructUsing(name => kernel.Get<IController>());
                    s.WhenStarted(controller =>
                    {
                        try
                        {
                            controller.Start();
                        }
                        catch (StratusException ex)
                        {
                            logger.FatalException(ex.Message, ex);
                            return;
                        }

                    });
                    s.WhenStopped(controller => controller.Stop());

                });
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription(serviceDescription);
                x.SetDisplayName(serviceDisplayName);
                x.SetServiceName(serviceName);

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

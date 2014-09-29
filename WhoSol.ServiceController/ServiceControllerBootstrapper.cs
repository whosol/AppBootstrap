﻿using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Extensions.Logging.Log4net;
using System;
using System.IO;
using System.Reflection;
using Topshelf;
using WhoSol.Contracts;
using WhoSol.Contracts.Constants;
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

            StartService();
        }

        public void Stop()
        {

        }

        private static void InitialiseConfiguration()
        {
            configuration = kernel.Get<IConfiguration>();

            configuration.Set(Config.RootDirectory, AssemblyDirectory);
            configuration.Set(Config.PluginDirectory, AssemblyDirectory + "Plugins\\");
            configuration.Set(Config.ThirdPartyDirectory, AssemblyDirectory + "ThirdParty\\");
            configuration.Set(Config.LogDirectory, AssemblyDirectory + "Logs\\");
        }

        private static void CreateKernel()
        {

            kernel = new StandardKernel(new ServiceControllerModule(), new Log4NetModule());

            InitialiseConfiguration();

            CreateLogger();

            LoadAssemblies(Config.ThirdPartyDirectory, "*ThirdParty.dll");

            LoadAssemblies(Config.PluginDirectory, "*Plugin.dll");

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
                assemblySearchFilter = string.Format("{0}{1}", configuration.Get<string>(Config.RootDirectory), filter);
            }

            kernel.Load(assemblySearchFilter);
            logger.Debug(string.Format("Loaded assemblies from {0}", assemblySearchFilter));
        }

        private static void CreateLogger()
        {
            var loggerFactory = kernel.Get<Ninject.Extensions.Logging.ILoggerFactory>();
            logger = loggerFactory.GetCurrentClassLogger();
            
            ConfigureLogger();
        }

        private static void ConfigureLogger()
        {
            if (!Directory.Exists(configuration.Get<string>(Config.LogDirectory)))
            {
                Directory.CreateDirectory(configuration.Get<string>(Config.LogDirectory));
            }

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders(); /*Remove any other appenders*/

            PatternLayout pl = new PatternLayout
            {
                ConversionPattern = "%date{dd MMM yyyy HH:mm:ss.fff} [%2%t] %-5p [%-10c] %m%n"
            };
            pl.ActivateOptions();

            RollingFileAppender rollingFileAppender = new RollingFileAppender
            {
                File = configuration.Get<string>(Config.LogDirectory) + "log.txt",
                AppendToFile = false,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 10,
                MaximumFileSize = "50GB",
                LockingModel = new FileAppender.MinimalLock(),
                Layout = pl
            };
            rollingFileAppender.ActivateOptions();

            BasicConfigurator.Configure(rollingFileAppender);
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

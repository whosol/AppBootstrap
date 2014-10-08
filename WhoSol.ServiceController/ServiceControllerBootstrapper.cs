using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using Ninject;
using Ninject.Extensions.Logging.Log4net;
using System;
using System.Collections.Generic;
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
        private Ninject.Extensions.Logging.ILogger logger;
        private IKernel kernel;
        private IConfiguration configuration;
        private readonly string serviceName;
        private readonly string serviceDisplayName;
        private readonly string serviceDescription;
        private readonly bool eventLog;
        private readonly bool consoleLog;
        private readonly string[] dependencies;

        public ServiceControllerBootstrapper(string serviceName, string serviceDisplayName, string serviceDescription, bool eventLog, bool consoleLog, params string[] dependencies)
        {
            this.serviceName = serviceName;
            this.serviceDisplayName = serviceDisplayName;
            this.serviceDescription = serviceDescription;
            this.eventLog = eventLog;
            this.consoleLog = consoleLog;
            this.dependencies = dependencies;
        }

        public void Start()
        {
            CreateKernel();

            StartService();
        }

        public void Stop()
        {

        }

        private void InitialiseConfiguration()
        {
            configuration = kernel.Get<IConfiguration>();

            configuration.Set(Config.RootDirectory, AssemblyDirectory);
            configuration.Set(Config.PluginDirectory, AssemblyDirectory + "Plugins\\");
            configuration.Set(Config.ThirdPartyDirectory, AssemblyDirectory + "ThirdParty\\");
            configuration.Set(Config.ApplicationName, serviceDisplayName);
            configuration.Set(Config.LogDirectory, AssemblyDirectory + "Logs\\");
        }

        private void CreateKernel()
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
                foreach (var dependency in dependencies)
                {
                    x.DependsOn(dependency);
                }
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription(serviceDescription);
                x.SetDisplayName(serviceDisplayName);
                x.SetServiceName(serviceName);

            });
        }

        private void LoadAssemblies(string directoryKey, string filter)
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
            logger.Info(string.Format("Loaded assemblies from {0}", assemblySearchFilter));
        }

        private void CreateLogger()
        {
            var loggerFactory = kernel.Get<Ninject.Extensions.Logging.ILoggerFactory>();
            logger = loggerFactory.GetCurrentClassLogger();

            ConfigureLogger();
        }

        private void ConfigureLogger()
        {
            var appenders = new List<IAppender>();

            if (!Directory.Exists(configuration.Get<string>(Config.LogDirectory)))
            {
                Directory.CreateDirectory(configuration.Get<string>(Config.LogDirectory));
            }

            var hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders(); /*Remove any other appenders*/

            var pl = new PatternLayout
            {
                ConversionPattern = "%date{dd MMM yyyy HH:mm:ss.fff} [%2%t] %-5p [%-10c] %m%n"
            };
            pl.ActivateOptions();

            var rollingFileAppender = new RollingFileAppender
            {
                File = configuration.Get<string>(Config.LogDirectory) + configuration.Get<string>(Config.ApplicationName) + ".log",
                AppendToFile = false,
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaxSizeRollBackups = 10,
                MaximumFileSize = "50GB",
                LockingModel = new FileAppender.MinimalLock(),
                Layout = pl
            };
            rollingFileAppender.ActivateOptions();

            appenders.Add(rollingFileAppender);

            if (eventLog)
            {
                var eventLogAppender = new EventLogAppender
                {
                    ApplicationName = configuration.Get<string>(Config.ApplicationName),
                    LogName = "Application",
                    Layout = pl
                };
                eventLogAppender.ActivateOptions();
                appenders.Add(eventLogAppender);
            }
            if (consoleLog)
            {
                var consoleLogAppender = new ColoredConsoleAppender
                {
                    Threshold = Level.All,
                    Layout = pl
                };
                consoleLogAppender.AddMapping(new ColoredConsoleAppender.LevelColors
                {
                    Level = Level.Debug,
                    ForeColor = ColoredConsoleAppender.Colors.Cyan
                        | ColoredConsoleAppender.Colors.HighIntensity
                });
                consoleLogAppender.AddMapping(new ColoredConsoleAppender.LevelColors
                {
                    Level = Level.Info,
                    ForeColor = ColoredConsoleAppender.Colors.Green
                        | ColoredConsoleAppender.Colors.HighIntensity
                });
                consoleLogAppender.AddMapping(new ColoredConsoleAppender.LevelColors
                {
                    Level = Level.Warn,
                    ForeColor = ColoredConsoleAppender.Colors.Yellow
                        | ColoredConsoleAppender.Colors.HighIntensity
                });
                consoleLogAppender.AddMapping(new ColoredConsoleAppender.LevelColors
                {
                    Level = Level.Error,
                    ForeColor = ColoredConsoleAppender.Colors.Red
                        | ColoredConsoleAppender.Colors.HighIntensity
                });
                consoleLogAppender.AddMapping(new ColoredConsoleAppender.LevelColors
                {
                    Level = Level.Fatal,
                    ForeColor = ColoredConsoleAppender.Colors.White
                        | ColoredConsoleAppender.Colors.HighIntensity,
                    BackColor = ColoredConsoleAppender.Colors.Red
                });
                consoleLogAppender.ActivateOptions();
                appenders.Add(consoleLogAppender);
            }

            BasicConfigurator.Configure(appenders.ToArray());
        }

        private string AssemblyDirectory
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

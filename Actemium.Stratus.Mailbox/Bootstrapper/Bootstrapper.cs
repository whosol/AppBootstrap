using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Manage;
using Actemium.Stratus.MailboxPlugin.Parse;
using Actemium.Stratus.MailboxPlugin.Receive;
using Actemium.Stratus.MailboxPlugin.Respond;
using Actemium.Stratus.MailboxPlugin.Write;
using Appccelerate.Bootstrapper;
using Appccelerate.EventBroker;

namespace Actemium.Stratus.MailboxPlugin.Bootstrapper
{
    public static class Bootstrapper
    {
        #region Fields

        private static DefaultBootstrapper<IShopFloorResultsExtension> bootstrapper;
        private static EventBroker eventBroker;
        private static ShopFloorResultsStrategy strategy;
        private static LogManager logManager;
        #endregion

        #region Public Methods

        public static void Start()
        {
            logManager = new LogManager();
            bootstrapper = new DefaultBootstrapper<IShopFloorResultsExtension>();
            eventBroker = new EventBroker();
            strategy = new ShopFloorResultsStrategy();

            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Starting Bootstrapper" });
            
            bootstrapper.Initialize(strategy);

            RegisterExtension(new TcpManager());

            RegisterExtension(new DirectoryManager());

            RegisterExtension(new DirectoryReceiver());

            RegisterExtension(new TcpReceiver());

            RegisterExtension(new TcpResponder());

            RegisterExtension(new WebApiWriter());

            RegisterExtension(logManager);

            RegisterExtension(new Rds3Parser());

            RegisterExtension(new Rds1Parser());

            RegisterExtension(new ConfigManager());

            bootstrapper.Run();
            
            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Started Bootstrapper" });

        }

        public static void Stop()
        {
            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Stopping Bootstrapper" });

            bootstrapper.Shutdown();
            
            bootstrapper.Dispose();
            eventBroker.Dispose();
            strategy.Dispose();

            bootstrapper = null;
            eventBroker = null;
            strategy = null;
            
            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Stopped Bootstrapper" });
        }

        #endregion

        #region Private Methods

        private static void RegisterExtension(IShopFloorResultsExtension extension)
        {
            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Registering " + extension.Describe() });

            eventBroker.Register(extension);
            bootstrapper.AddExtension(extension);
            
            logManager.LogHandler("Bootstrapper", new LogEventArgs { Level = LogLevel.Information, Message = "Registered " + extension.Describe() });
        }

        #endregion
    }
}

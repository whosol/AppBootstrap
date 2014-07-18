using Actemium.Stratus.Contracts;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.Utilities;
using Appccelerate.EventBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public class ConfigManager : ShopFloorResultsExtensionBase, IManager
    {
        #region Fields

        private Dictionary<ConfigSection, Dictionary<ConfigKey, object>> configuration;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private Task parseConfigFileTask;
        private readonly IConfiguration config;
        #endregion

        #region Events

        [EventPublication("topic://ConfigChanged", HandlerRestriction.Asynchronous)]
        public event EventHandler<ConfigChangedEventArgs> ConfigChangedEvent;

        #endregion

        #region Constructors

        public ConfigManager(IConfiguration config)
        {
            this.config = config;
        }

        private async Task ParseConfigFile(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                this.configuration = ConfigFile<ConfigSection, ConfigKey>.Parse(this, config.Get<string>("PluginDirectory"), "PluginSettings");

                if (ConfigFile<ConfigSection, ConfigKey>.Updated)
                {
                    foreach (var section in configuration.Keys)
                    {
                        LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = section.ToString() + " configuration section read" });

                        if (ConfigChangedEvent != null)
                            ConfigChangedEvent(this, new ConfigChangedEventArgs { ChangedSection = section, Configuration = configuration[section] });
                    }

                }
                await Task.Delay(30000);
            }
        }

        #endregion

        #region Properties

        public ConfigSection[] ConfigSections
        {
            get { return configuration.Keys.ToArray(); }
        }

        public object this[ConfigSection section, ConfigKey key]
        {
            get
            {
                return configuration[section][key];
            }
            set
            {
                AddItem(section, key, value);
            }
        }

        #endregion

        #region Public Overrides

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Starting " + Describe() });

            parseConfigFileTask = ParseConfigFile(cts.Token);

            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });

        }

        public override string Describe()
        {
            return "Configuration Management Extension";
        }

        #endregion

        #region IManager Interface

        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            //Nothing to do here!
        }

        #endregion

        #region Private Methods

        private void AddItem(ConfigSection section, ConfigKey key, object value)
        {
            if (!configuration.ContainsKey(section))
            {
                configuration[section] = new Dictionary<ConfigKey, object>();
            }

            configuration[section][key] = value;

            if (ConfigChangedEvent != null)
                ConfigChangedEvent(this, new ConfigChangedEventArgs { ChangedSection = section, Configuration = configuration[section] });
        }

        #endregion

        public event EventHandler<LogEventArgs> LogEvent;

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopping " + Describe() });

            cts.Cancel();
            try
            {
                Task.WaitAll(new[] { parseConfigFileTask }, 100, cts.Token);
            }
            catch (OperationCanceledException)
            {
                LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Cancelled ConfigManager Tasks"});
            }
            catch (AggregateException ae)
            {
                ae.Handle(x =>
                {
                    if (x is TaskCanceledException)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }

            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });

        }
    }
}

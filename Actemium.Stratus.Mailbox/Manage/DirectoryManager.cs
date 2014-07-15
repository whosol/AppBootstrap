using System.IO;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using System;

namespace Actemium.Stratus.MailboxPlugin.Manage
{
    public class DirectoryManager : ShopFloorResultsExtensionBase, IManager
    {
        public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
        {
            if (e.ChangedSection == ConfigSection.InputDirectories)
            {
                var directories = (e.Configuration[ConfigKey.ResultDirectories] as string).Split(';', ',', ' ');

                if (directories != null)
                {
                    foreach (var directory in directories)
                    {
                        CheckAndCreateDirectory(directory);
                    }
                }
            }

            if (e.ChangedSection == ConfigSection.OutputDirectories)
            {
                var baseDirectory = e.Configuration[ConfigKey.BaseDirectory] as string;

                CheckAndCreateDirectory(baseDirectory);

                CheckAndCreateDirectory(baseDirectory + "\\" + e.Configuration[ConfigKey.Bad] as string);

                CheckAndCreateDirectory(baseDirectory + "\\" + e.Configuration[ConfigKey.Duplicate] as string);
            }
        }

        private void CheckAndCreateDirectory(string directory)
        {
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public override string Describe()
        {
            return "Directory Management Extension";
        }




        public event EventHandler<LogEventArgs> LogEvent;

        public override void Start()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Started " + Describe() });
        }

        public override void Stop()
        {
            LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });
        }
    }
}

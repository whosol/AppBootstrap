using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WhoSol.Contracts;
using WhoSol.Contracts.Base;
using WhoSol.Contracts.Constants;
using WhoSol.DirectoryScannerPlugin.Properties;

namespace WhoSol.DirectoryScannerPlugin
{
    public class PolledDirectoryScannerPlugin : BasePlugin, IDirectoryScanner
    {
        private Dictionary<string, string> directories;
        private List<string> foundFiles;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken ct;
        private string processedFile;
        private int delay = 30000;
        private Timer pollTimer;

        public PolledDirectoryScannerPlugin(ILogger logger, IConfiguration configuration)
            : base(logger, configuration)
        {
            foundFiles = new List<string>();
            cts = new CancellationTokenSource();
            ct = cts.Token;
            Autostart = false;
        }

        public string LogFile { get; set; }

        public string ProcessedFile
        {
            get
            {
                return processedFile;
            }
            set
            {
                processedFile = Path.Combine(configuration.Get<string>(Config.LogDirectory), value);
            }
        }

        public string[] Directories
        {
            get { return directories.Keys.ToArray(); }
        }

        public void AddDirectory(string path, string filter)
        {
            logger.Info(Resources.AddingDirectory, path, filter);
            if (directories == null)
            {
                directories = new Dictionary<string, string>();
            }
            directories[path] = filter;
        }

        public override void Start(params object[] args)
        {
            if (args.Length == 1)
            {
                delay = int.Parse(args[0] as string) * 1000;
            }

            logger.Info(Resources.DirectoryPollRate, delay / 1000);

            if (!string.IsNullOrEmpty(ProcessedFile) && File.Exists(ProcessedFile))
            {
                foundFiles.AddRange(File.ReadAllLines(ProcessedFile));
                logger.Info(Resources.FoundAlreadyParsed, foundFiles.Count);
            }


            pollTimer = new Timer(new TimerCallback(ReadDirectories));

            pollTimer.Change(0, delay);
            logger.Info(Resources.StartedPlugin);
        }

        private void ReadDirectories(object state)
        {
            foreach (var directory in directories)
            {
                try
                {
                    if (!Directory.Exists(directory.Key))
                    {
                        logger.Warn(Resources.DirectoryDoesNotExist, directory.Key);
                        continue;
                    }

                    var files = Directory.EnumerateFiles(directory.Key, directory.Value);

                    var newFiles = files.Except(foundFiles);

                    if (newFiles != null && newFiles.Count() > 0)
                    {
                        logger.Info(Resources.NewFilesFound, newFiles.Count(), directory);

                        foundFiles = files.ToList();

                        FoundFiles(newFiles);

                        if (!string.IsNullOrEmpty(ProcessedFile) && newFiles != null)
                        {
                            File.AppendAllLines(ProcessedFile, newFiles);
                        }
                    }
                    else
                    {
                        logger.Info(Resources.NoNewFilesFound, directory.Key, directory.Value);
                    }

                }
                catch (Exception ex)
                {
                    logger.ErrorException(Resources.PluginTaskException, ex);
                }
            }
        }

        public override void Stop()
        {
            cts.Cancel();

            logger.Info(Resources.StoppedPlugin);
        }

        public event Action<IEnumerable<string>> FoundFiles;


        public override string Description
        {
            get
            {
                return Resources.PluginDescription;
            }
        }

    }
}

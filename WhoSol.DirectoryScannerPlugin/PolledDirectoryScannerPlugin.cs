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

namespace WhoSol.DirectoryScannerPlugin
{
    public class PolledDirectoryScannerPlugin : BasePlugin, IDirectoryScanner
    {
        private Dictionary<string, string> directories;
        private List<string> foundFiles;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken ct;
        private string processedFile;

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
            if (directories == null)
            {
                directories = new Dictionary<string, string>();
            }
            directories[path] = filter;
        }

        public override void Start(params object[] args)
        {
            int delay = 30000;

            if (args.Length == 1)
            {
                delay = int.Parse(args[0] as string) * 1000;
            }

            if (!string.IsNullOrEmpty(ProcessedFile) && File.Exists(ProcessedFile))
            {
                foundFiles.AddRange(File.ReadAllLines(ProcessedFile));
            }

            Task.Factory.StartNew(async () =>
            {
                while (!ct.IsCancellationRequested)
                {
                    foreach (var directory in directories)
                    {
                        if (!Directory.Exists(directory.Key))
                        {
                            continue;
                        }

                        var files = Directory.EnumerateFiles(directory.Key, directory.Value);

                        var newFiles = files.Except(foundFiles);

                        foundFiles = files.ToList();

                        if (newFiles.Count() > 0)
                        {
                            if (!string.IsNullOrEmpty(ProcessedFile))
                            {
                                File.AppendAllLines(ProcessedFile, newFiles);
                            }
                            FoundFiles(newFiles);
                        }

                    }
                    await Task.Delay(delay, ct);
                }
            }, ct);
        }

        public override void Stop()
        {
            cts.Cancel();
        }

        public event Action<IEnumerable<string>> FoundFiles;


        public override string Description
        {
            get { return "Polled Directory Scanner Plugin"; }
        }
    }
}

using WhoSol.Contracts.Enums;
using Actemium.Stratus.MailboxPlugin.Bootstrapper;
using Actemium.Stratus.MailboxPlugin.Enums;
using Actemium.Stratus.MailboxPlugin.Events;
using Actemium.Stratus.MailboxPlugin.Parse;
using WhoSol.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Actemium.Stratus.MailboxPlugin.Receive
{
	public sealed class DirectoryReceiver : ShopFloorResultsExtensionBase, IReceiver, IDisposable
	{
		#region Fields

		private readonly Dictionary<string, Task> polledDirectories = new Dictionary<string, Task>();
		private readonly UniqueQueue<string> files = new UniqueQueue<string>();
		private static readonly CancellationTokenSource cts = new CancellationTokenSource();
		private Task dequeueTask;

		#endregion

		#region Properties

		public string[] MonitoredDirectories
		{
			get { return polledDirectories.Keys.ToArray(); }
		}

		#endregion

		#region Constructors

		public DirectoryReceiver(params string[] directoriesToMonitor)
		{
			foreach (var polledDirectory in directoriesToMonitor)
			{
				AddDirectoryToMonitor(polledDirectory);
			}
		}

		#endregion

		#region Private Methods

		private bool AddDirectoryToMonitor(string directoryToMonitor)
		{
			if (!polledDirectories.ContainsKey(directoryToMonitor))
			{
				if (Directory.Exists(directoryToMonitor))
				{
					LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Adding " + directoryToMonitor + " to monitored directories" });
					polledDirectories.Add(directoryToMonitor, MonitorDirectory(directoryToMonitor, 30, cts.Token));
					return true;
				}
				else
				{
					LogEvent(this, new LogEventArgs { Level = LogLevel.Warning, Message = directoryToMonitor + " does not exist" });
				}
			}
			LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = directoryToMonitor + " is already monitored" });
			return false;
		}

		private void MoveFileToProcessed(string file)
		{
			if (!Directory.Exists(Path.GetDirectoryName(file) + "\\Processed"))
			{
				LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Creating " + Path.GetDirectoryName(file) + "\\Processed directory" });
				Directory.CreateDirectory(Path.GetDirectoryName(file) + "\\Processed");
			}
			else
			{
				LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = Path.GetDirectoryName(file) + "\\Processed directory exists" });
			}

			if (!File.Exists(Path.GetDirectoryName(file) + "\\Processed\\" + Path.GetFileName(file)))
			{
				LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Moving " + Path.GetFileName(file) + " to processed" });
				File.Move(file, Path.GetDirectoryName(file) + "\\Processed\\" + Path.GetFileName(file));
			}
		}

		private Task DequeueFiles(CancellationToken ct)
		{
			return Task.Factory.StartNew(async () =>
				{
					string fileName = null;
					while (!ct.IsCancellationRequested)
					{
						//The queue will block the thread awaiting an item to dequeue
						fileName = files.Dequeue();
						LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = fileName + " received" });

						if (fileName != null && File.Exists(fileName))
						{
							var status = await ReadFile(fileName);
							if (status == PersistStatus.OK)
							{
								MoveFileToProcessed(fileName);
							}
							fileName = null;
						}
					}
				}, TaskCreationOptions.LongRunning);
		}

		private async Task<PersistStatus> ReadFile(string fullPath)
		{
			try
			{
				LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Opening " + fullPath });

				FileStream stream = null;
				int attempts = 0;
				while (stream == null && attempts++ < 3)
				{
					stream = TryOpenFile(fullPath);
					await Task.Delay(10);
				}
				using (stream)
				{
					LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Opened " + fullPath });

					var buffer = new byte[stream.Length];
					var bytesRead = await stream.ReadAsync(buffer, 0, (int)stream.Length);

					LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = bytesRead + " bytes read from " + fullPath });

					if (bytesRead != stream.Length)
					{
						LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = fullPath + " is corrupted" });
						return PersistStatus.CorruptData;
					}

					var parseEventArgs = new ParseStringToXmlEventArgs { Data = Encoding.ASCII.GetString(buffer) };

					ParseStringToXmlRequest(this, parseEventArgs);

					var xml = parseEventArgs.Xml;

					if (xml == null)
					{
						LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = fullPath + " is in an unknown format" });
						return PersistStatus.InvalidFormat;
					}

					if (XDocumentAvailableEvent != null)
					{
						var eventArgs = new XDocumentAvailableEventArgs { Data = xml };
						LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Persisting " + fullPath + " in DB" });

						XDocumentAvailableEvent(this, eventArgs);

						LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Persist Status : " + eventArgs.PersistStatus });

						return eventArgs.PersistStatus;
					}
					return PersistStatus.NoPersistanceMethod;
				}
			}
			catch (ApplicationException)
			{
				files.Enqueue(fullPath);
				return PersistStatus.InvalidFormat;
			}
		}

		private async Task MonitorDirectory(string polledDirectory, int pollRate, CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				Parallel.ForEach(Directory.EnumerateFiles(polledDirectory, "*.xml"), (file) => files.Enqueue(file));
				await Task.Delay(pollRate * 1000, ct);
			}
		}

		private FileStream TryOpenFile(string fileName)
		{
			try
			{
				var stream = new FileStream(fileName, FileMode.Open);
				return stream;
			}
			catch (IOException)
			{
				return null;
			}
			catch (UnauthorizedAccessException uae)
			{
				throw new ApplicationException(string.Format("Cannot access {0} due to lack of file permissions.", fileName), uae);
			}

		}

		#endregion

		#region Public Overrides

		public override void Start()
		{
			dequeueTask = DequeueFiles(cts.Token);
		}

		public override void Stop()
		{
			LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopping " + Describe() });

			cts.Cancel();
			try
			{
				Task.WaitAll(polledDirectories.Values.ToArray(), 100);
			}
			catch (AggregateException ae)
			{
				//Exnsure the exceptions from the tasks are only cancel exceptions
				ae.Handle((x) =>
					{
						return x is TaskCanceledException ? true : false;
					});
			}
			dequeueTask.Wait(100);

			LogEvent(this, new LogEventArgs { Level = LogLevel.Information, Message = "Stopped " + Describe() });

		}

		public override string Describe()
		{
			return "Directory Receiver";
		}

		#endregion

		#region IReceiver Interface

		public event EventHandler<XDocumentAvailableEventArgs> XDocumentAvailableEvent;

		public event EventHandler<ParseStringToXmlEventArgs> ParseStringToXmlRequest;

		public event EventHandler<LogEventArgs> LogEvent;

		public void ConfigChangedHandler(object sender, ConfigChangedEventArgs e)
		{
			if (e.ChangedSection != ConfigSection.InputDirectories)
				return;

			if (e.Configuration[ConfigKey.ResultDirectories] is string)
			{
				foreach (var item in (e.Configuration[ConfigKey.ResultDirectories] as string).Split(';', ',', ' '))
				{
					AddDirectoryToMonitor(item);
				}
			}
		}

		#endregion

		#region IDisposable Interface

		public void Dispose()
		{
			files.Dispose();
			cts.Dispose();
			dequeueTask.Dispose();
		}

		#endregion
	 
	}
}

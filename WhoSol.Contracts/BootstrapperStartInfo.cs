
namespace WhoSol.Contracts
{
    public class BootstrapperStartInfo
    {
        public string ServiceName { get; set; }

        public string[] Dependencies { get; set; }

        public string LogFile { get; set; }

        public bool ConsoleLog { get; set; }

        public bool EventLog { get; set; }

        public string ServiceDescription { get; set; }

        public string ServiceDisplayName { get; set; }
    }
}

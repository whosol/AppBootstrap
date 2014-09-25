using System;
using System.Collections.Generic;

namespace WhoSol.Contracts
{
    public interface IDirectoryScanner
    {
        string[] Directories { get; }
        string ProcessedFile { get; set; }
        void AddDirectory(string path, string filter = "*.*");
        void Start(params object[] args);
        void Stop();

        event Action<IEnumerable<string>> FoundFiles;

    }
}

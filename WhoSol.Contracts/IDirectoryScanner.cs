using System;
using System.Collections.Generic;

namespace WhoSol.Contracts
{
    public interface IDirectoryScanner
    {
        string[] Directories { get; }
        void AddDirectory(string path, string filter = "*.*");
        void Start();
        void Stop();
        
        event Action<List<string>> FoundFiles;
    }
}

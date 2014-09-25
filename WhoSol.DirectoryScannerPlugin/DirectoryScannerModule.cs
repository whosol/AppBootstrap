using Ninject.Modules;
using System;
using WhoSol.Contracts;

namespace WhoSol.DirectoryScannerPlugin
{
    public class DirectoryScannerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<PolledDirectoryScannerPlugin>();
            Bind<IDirectoryScanner>().To<PolledDirectoryScannerPlugin>();
        }
    }
}

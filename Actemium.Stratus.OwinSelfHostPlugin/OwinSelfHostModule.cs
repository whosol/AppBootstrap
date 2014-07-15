using Actemium.Stratus.Contracts;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actemium.Stratus.OwinSelfHostPlugin
{
    public class OwinSelfHostModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlugin>().To<OwinSelfHostPlugin>().InSingletonScope();
        }
    }
}

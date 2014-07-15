using Actemium.Stratus.Contracts;
using Ninject;
using Topshelf;

namespace Actemium.Stratus.ServiceController
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new ServiceControllerModule());
            kernel.Load("*.Stratus.*Plugin.dll");
            
            HostFactory.Run(x =>
            {
                x.Service<IController>(s => 
                {
                    s.ConstructUsing(name => kernel.Get<IController>());
                    s.WhenStarted(controller => controller.Start());
                    s.WhenStopped(controller => controller.Stop());
                });
                x.RunAsLocalSystem();                   

                x.SetDescription("Service Controller");     
                x.SetDisplayName("Service Controller");              
                x.SetServiceName("FASSC");    
            });
        }
    }
}

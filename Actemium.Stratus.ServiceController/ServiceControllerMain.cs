using Actemium.Stratus.Contracts;

namespace Actemium.Stratus.ServiceController
{
    public class ServiceControllerMain : IController
    {
        private readonly IPlugin[] plugins;

        public ServiceControllerMain(IPlugin[] plugins)
        {
            this.plugins = plugins;
        }

        public void Stop()
        {
            foreach (var plugin in plugins)
            {
                //    Console.WriteLine("Name:" + plugin.Name + " Description:" + plugin.Description);
                plugin.Stop();
            }
        }

        public void Start()
        {
            foreach (var plugin in plugins)
            {
                //    Console.WriteLine("Name:" + plugin.Name + " Description:" + plugin.Description);
                plugin.Start();
            }
        }
    }
}

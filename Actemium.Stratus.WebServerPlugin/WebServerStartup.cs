using Actemium.Stratus.Contracts;
using Microsoft.Owin.FileSystems;
using Ninject;
using Owin;
using System.IO;
using System.Reflection;

namespace Actemium.Stratus.WebServerPlugin
{
    public class WebServerStartup : IStartup
    {
        private readonly IKernel kernel;
        private readonly IConfiguration configuration;

        public WebServerStartup(IKernel kernel, IConfiguration configuration)
        {
            this.kernel = kernel;
            this.configuration = configuration;
        }

        public void Configuration(IAppBuilder app)
        {
            //var staticFilesDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Web");
            //app.UseStaticFiles(new Microsoft.Owin.StaticFiles.StaticFileOptions { FileSystem = new PhysicalFileSystem(staticFilesDir)});
        }
    }
}

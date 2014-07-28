using WhoSol.Contracts;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.IO;

namespace WhoSol.WebServerPlugin
{
    public class WebServerStartup : IStartup
    {
        private readonly IConfiguration configuration;

        public WebServerStartup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configuration(IAppBuilder app)
        {
            var staticFilesDir = Path.Combine(configuration.Get<string>("PluginDirectory"), "Web");


            var clientOptions = new FileServerOptions
            {
                RequestPath = new PathString(""),
                FileSystem = new PhysicalFileSystem(staticFilesDir),
                EnableDefaultFiles = true,
                EnableDirectoryBrowsing = false
            };
            clientOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            clientOptions.StaticFileOptions.ServeUnknownFileTypes = true;

            app.UseFileServer(clientOptions);
        }
    }
}

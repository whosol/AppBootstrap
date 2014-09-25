using WhoSol.Contracts;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System.IO;
using Ninject.Extensions.Logging;
using WhoSol.Contracts.Constants;

namespace WhoSol.WebServerPlugin
{
    public class WebServerStartup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public WebServerStartup(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public void Configuration(IAppBuilder app)
        {
            var staticFilesDir = Path.Combine(configuration.Get<string>(Config.PluginDirectory), "Web");

            if (Directory.Exists(staticFilesDir))
            {
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
                logger.Info("Web Server Plugin Started");
            }
            else
            {
                logger.Warn("No Web Pages to serve. Web Server Plugin Not Started");
            }
        }
    }
}

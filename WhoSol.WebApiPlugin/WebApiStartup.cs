 using WhoSol.Contracts;
using WhoSol.WebApiPlugin.Formatters;
using Ninject;
using Owin;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Ninject.Extensions.Logging;
using System.Web.Http.Dispatcher;

namespace WhoSol.WebApiPlugin
{
    public class WebApiStartup : IStartup
    {
        private readonly IKernel kernel;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;

        public WebApiStartup(IKernel kernel, IConfiguration configuration, ILogger logger)
        {
            this.kernel = kernel;
            this.configuration = configuration;
            this.logger = logger;
        }

        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = new HttpConfiguration();

            webApiConfiguration.DependencyResolver = new NinjectDependencyResolver(this.kernel);

            webApiConfiguration.MapHttpAttributeRoutes();

            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",

                defaults: new { id = RouteParameter.Optional, controller = "test" }
                );

            webApiConfiguration.Formatters.Insert(0, new XmlFormatter());

            webApiConfiguration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

            app.UseWebApi(webApiConfiguration);

            logger.Info("WebAPI Plugin started");
        }
    }
}

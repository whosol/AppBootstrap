using Actemium.Stratus.Contracts;
using Actemium.Stratus.WebApiPlugin.Formatters;
using Ninject;
using Owin;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Actemium.Stratus.WebApi
{
    public class WebApiStartup : IStartup
    {
        private readonly IKernel kernel;
        private readonly IConfiguration configuration;

        public WebApiStartup(IKernel kernel, IConfiguration configuration)
        {
            this.kernel = kernel;
            this.configuration = configuration;
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

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            app.UseWebApi(webApiConfiguration);
        }
    }
}

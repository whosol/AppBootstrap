using Actemium.Stratus.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Actemium.Stratus.ThirdParty.Controllers
{
    public class ThirdPartyController : ApiController
    {
        IConfiguration config;
        public ThirdPartyController(IConfiguration config)
        {
            this.config = config;
        }

        public ThirdPartyLibraries Get()
        {
            return new ThirdPartyLibraries
            {
                Libraries = Directory.EnumerateFiles(config.Get<string>("ThirdPartyDirectory"), "*.dll")
                .Where(file => !string.IsNullOrEmpty(file) && !file.Contains("Actemium.Stratus.ThirdParty.dll"))
                .Select(file =>
                {
                    var assembly = Assembly.LoadFrom(file);
                    return new ThirdPartyLibrary
                    {
                        Name = assembly.GetName().Name,
                        Version = assembly.GetName().Version.ToString(),
                        Location = assembly.Location
                    };
                })
                .ToArray()
            };
        }
    }
}

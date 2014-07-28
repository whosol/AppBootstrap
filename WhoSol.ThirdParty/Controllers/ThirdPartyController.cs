using WhoSol.Contracts;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace WhoSol.ThirdParty.Controllers
{
    public class ThirdPartyController : ApiController
    {
        private readonly IConfiguration config;
        public ThirdPartyController(IConfiguration config)
        {
            this.config = config;
        }

        public ThirdPartyLibraries Get()
        {
            var ret = new ThirdPartyLibraries
            {
                Libraries = Directory.EnumerateFiles(config.Get<string>("ThirdPartyDirectory"), "*.dll")
                .Where(file => !string.IsNullOrEmpty(file) && !file.Contains("WhoSol.ThirdParty.dll"))
                .Select(file =>
                {
                    var assembly = Assembly.LoadFrom(file);
                    
                    return new ThirdPartyLibrary
                    {
                        Name = assembly.GetName().Name,
                        Version = assembly.GetName().Version.ToString(),
                        FileVersion =  FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion,
                        Location = assembly.Location
                    };
                })
                .ToArray()
            };
            return ret;
        }
    }
}

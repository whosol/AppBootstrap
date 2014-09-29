using Ninject.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using WhoSol.Contracts;
using WhoSol.Contracts.Constants;
using WhoSol.ServiceController.Controllers.Dto;
using WhoSol.ServiceController.Enums;

namespace WhoSol.ServiceController.Controllers
{
    public class LogsController : ApiController
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public LogsController(ILogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public LogsDto Get()
        {
            return new LogsDto
            {
                LogEntry = File.ReadAllLines(configuration.Get<string>(Config.LogDirectory) + "log.txt").Select(line =>
                {
                    var entry = line.Split('[', ']');
                    return new LogDto
                    {
                        DateTime = DateTime.Parse(entry[0].Trim()),
                        Thread = int.Parse(entry[1].Trim()),
                        LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), entry[2].Trim(), true),
                        Class = entry[3].Trim(),
                        Message = entry[4].Trim(),
                    };
                })
            };
        }
    }
}

using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                Logs = CreateDtos()
            };
        }

        public LogsDto Get(LogLevel level)
        {
            return new LogsDto
            {
                Logs = CreateDtos().Where(l => l.LogLevel.ToLower() == level.ToString().ToLower())
            };
        }

        private IEnumerable<LogDto> CreateDtos()
        {
            return File.ReadAllLines(configuration.Get<string>(Config.LogDirectory) + configuration.Get<string>(Config.LogFile))
                .Select(line =>
                {
                    var entry = line.Split('[', ']');
                    try
                    {
                        return new LogDto
                        {
                            Timestamp = entry[0].Trim(),
                            Thread = int.Parse(entry[1].Trim()),
                            LogLevel = entry[2].Trim(),
                            Class = entry[3].Trim(),
                            Message = entry[4].Trim(),
                        };
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(dto => dto != null)
                .OrderByDescending(dto => DateTime.Parse(dto.Timestamp));
        }
    }
}

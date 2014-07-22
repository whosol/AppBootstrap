using Actemium.Stratus.Contracts;
using Ninject.Extensions.Logging;
using System.Web.Http;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly IUnitOfWork uow;
        protected readonly ILogger logger;

        public BaseController(IUnitOfWork uow, ILogger logger)
        {
            this.uow = uow;
            this.logger = logger;
        }
    }
}

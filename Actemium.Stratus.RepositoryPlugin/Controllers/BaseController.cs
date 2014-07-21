using Actemium.Stratus.Contracts;
using System.Web.Http;

namespace Actemium.Stratus.RepositoryPlugin.Controllers
{
    public abstract class BaseController : ApiController
    {
        protected readonly IUnitOfWork uow;

        public BaseController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
    }
}

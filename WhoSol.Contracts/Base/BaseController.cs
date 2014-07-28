using Ninject.Extensions.Logging;
using System.Web.Http;

namespace WhoSol.Contracts.Base
{
    public abstract class BaseController<T1, T2, T3> : ApiController
    {
        protected readonly IUnitOfWork uow;
        protected readonly ILogger logger;

        public BaseController(IUnitOfWork uow, ILogger logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        public abstract T2 CreateDto(T1 dataObject);

        public abstract T3 Get();

        public abstract T2 Get(int id);
    }
}

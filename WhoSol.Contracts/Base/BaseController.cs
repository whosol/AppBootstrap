using Ninject.Extensions.Logging;
using System.Web.Http;

namespace WhoSol.Contracts.Base
{
    public abstract class BaseController<TData, TDto, TDtoWrapper> : ApiController
    {
        protected readonly IUnitOfWork uow;
        protected readonly ILogger logger;

        public BaseController(IUnitOfWork uow, ILogger logger)
        {
            this.uow = uow;
            this.logger = logger;
        }

        public abstract TDto CreateDto(TData dataObject);

        public abstract TDtoWrapper GetAll();

        public abstract TDto GetById(int id);
    }
}

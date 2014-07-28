using System;
using System.Linq;
using System.Web.Http;
using Ninject.Extensions.Logging;
using WhoSol.Contracts.Base;
using WhoSol.Contracts;

namespace Actemium.Stratus.Contracts.Base
{

    public abstract class StratusBaseController<T1, T2, T3> : BaseController<T1, T2, T3>
    {
        protected readonly IStratusUnitOfWork uow;
        protected readonly ILogger logger;

        public StratusBaseController(IStratusUnitOfWork uow, ILogger logger)
            : base((IUnitOfWork)uow, logger)
        {
            this.uow = uow;
            this.logger = logger;
        }
    }

}

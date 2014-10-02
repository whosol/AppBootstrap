using System.Data.SqlClient;

namespace WhoSol.Contracts.Base
{
    public class BaseUnitOfWork : IUnitOfWork
    {
        protected IUnitOfWork coreUoW;

        public BaseUnitOfWork(IUnitOfWork baseUoW)
        {
            this.coreUoW = baseUoW;
        }

        public virtual object Store
        {
            get
            {
                return coreUoW.Store;
            }
            set
            {
                coreUoW.Store = value;
            }
        }

        public virtual void Commit()
        {
            coreUoW.Commit();
        }

        public virtual int ExecuteSp(string p, params SqlParameter[] zoneParam)
        {
            return coreUoW.ExecuteSp(p, zoneParam);
        }

        public virtual bool Exists
        {
            get { return coreUoW.Exists; }
        }

        public virtual string ConnectionString
        {
            get { return coreUoW.ConnectionString; }
        }

        public virtual void Initialise(params object[] args)
        {
            coreUoW.Initialise(args);
        }

        public virtual void Dispose()
        {
            coreUoW.Dispose();
        }
    }
}

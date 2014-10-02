using System;
using System.Data.Entity;
using System.Data.SqlClient;
using WhoSol.Contracts;

namespace WhoSol.EntityFrameworkPlugin
{
    public sealed class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public void Commit()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ExecuteSp(string p, params System.Data.SqlClient.SqlParameter[] zoneParam)
        {
            return dbContext.Database.ExecuteSqlCommand(p, zoneParam);

        }

        public bool Exists
        {
            get { return dbContext.Database.Exists(); }
        }


        public string ConnectionString
        {
            get { return dbContext.Database.Connection.ConnectionString; }
        }

        public void Initialise(params object[] args)
        {
            try
            {
                dbContext.Database.Connection.Open();
            }
            catch (SqlException e)
            {

                throw e;
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }


        public object Store
        {
            get { return dbContext; }
            set
            {
                if (value is DbContext)
                {
                    dbContext = value as DbContext;
                }
                else {
                    throw new ArgumentException("Store is not of type DbContext");
                }
            }
        }
    }
}

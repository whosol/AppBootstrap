
using System;
using System.Data.SqlClient;

namespace WhoSol.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        //TODO: Add IRepository calls for the entity groups.

        void Commit();
        int ExecuteSp(string p, params SqlParameter[] zoneParam);
        bool Exists { get; }
        string ConnectionString { get; }

        void Initialise();
    }
}

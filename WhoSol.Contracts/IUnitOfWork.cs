
using System;
using System.Data.SqlClient;

namespace WhoSol.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        object Store { get; set; }
        void Commit();
        int ExecuteSp(string p, params SqlParameter[] zoneParam);
        bool Exists { get; }
        string ConnectionString { get; }
        void Initialise(params object[] args);
    }
}

﻿
using Actemium.Stratus.DataObjects;
using System;
using System.Data.SqlClient;

namespace Actemium.Stratus.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cell> Cells { get; }
        IRepository<Company> Companies { get; }
        IRepository<Location> Locations { get; }
        IRepository<Plant> Plants { get; }
        IRepository<Process> Processes { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductType> ProductTypes { get; }
        IRepository<Result> Results { get; }
        IRepository<ResultDescription> ResultDescriptions { get; }
        IRepository<Sequence> Sequences { get; }
        IRepository<SequenceExecution> SequenceExecutions { get; }
        IRepository<Tester> Testers { get; }
        IRepository<Visit> Visits { get; }
        IRepository<Zone> Zones { get; }
        void Commit();
        int ExecuteSp(string p, params SqlParameter[] zoneParam);
        bool Exists { get; }
        string ConnectionString { get; }

        void Initialise();
    }
}
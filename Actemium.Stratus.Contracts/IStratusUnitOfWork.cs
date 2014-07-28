using System;
using System.Linq;
using Actemium.Stratus.DataObjects;
using WhoSol.Contracts;

namespace Actemium.Stratus.Contracts
{
    public interface IStratusUnitOfWork : IUnitOfWork
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
    }
}

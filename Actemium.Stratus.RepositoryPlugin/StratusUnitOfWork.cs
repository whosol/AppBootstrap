using Actemium.Stratus.Contracts;
using Actemium.Stratus.Database;
using Actemium.Stratus.DataObjects;
using System;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Actemium.Stratus.RepositoryPlugin
{
    public sealed class StratusUnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly DbContext dbContext;
        // private ObjectContext objectContext;
        private IRepository<Cell> cells;
        private IRepository<Company> companies;
        private IRepository<Location> locations;
        private IRepository<Plant> plants;
        private IRepository<Process> processes;
        private IRepository<Product> products;
        private IRepository<ProductType> productTypes;
        private IRepository<Result> results;
        private IRepository<ResultDescription> resultDescriptions;
        private IRepository<Sequence> sequences;
        private IRepository<SequenceExecution> sequenceExecutions;
        private IRepository<Tester> testers;
        private IRepository<Visit> visits;
        private IRepository<Zone> zones;

        public StratusUnitOfWork()
        {
            dbContext = new StratusContext();
            dbContext.Configuration.LazyLoadingEnabled = false;
            dbContext.Configuration.AutoDetectChangesEnabled = false;
            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Database.CommandTimeout = 180;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public bool Exists
        {
            get { return dbContext.Database.Exists(); }
        }

        public string ConnectionString
        {
            get { return dbContext.Database.Connection.ConnectionString; }
        }

        public void Initialise()
        {
            dbContext.Database.Connection.Open();
        }

        public IRepository<Cell> Cells
        {
            get
            {
                if (cells == null)
                {
                    cells = new StratusRepository<Cell>(dbContext);
                }
                return cells;
            }
        }

        public IRepository<Company> Companies
        {
            get
            {

                if (companies == null)
                {
                    companies = new StratusRepository<Company>(dbContext);
                }
                return companies;
            }
        }

        public IRepository<Location> Locations
        {
            get
            {
                if (locations == null)
                {
                    locations = new StratusRepository<Location>(dbContext);
                }
                return locations;
            }
        }

        public IRepository<Plant> Plants
        {
            get
            {
                if (plants == null)
                {
                    plants = new StratusRepository<Plant>(dbContext);
                }
                return plants;
            }
        }

        public IRepository<Process> Processes
        {
            get
            {
                if (processes == null)
                {
                    processes = new StratusRepository<Process>(dbContext);
                }
                return processes;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (products == null)
                {
                    products = new StratusRepository<Product>(dbContext);
                }
                return products;
            }
        }

        public IRepository<ProductType> ProductTypes
        {
            get
            {
                if (productTypes == null)
                {
                    productTypes = new StratusRepository<ProductType>(dbContext);
                }
                return productTypes;
            }
        }

        public IRepository<Result> Results
        {
            get
            {
                if (results == null)
                {
                    results = new StratusRepository<Result>(dbContext);
                }
                return results;
            }
        }

        public IRepository<ResultDescription> ResultDescriptions
        {
            get
            {
                if (resultDescriptions == null)
                {
                    resultDescriptions = new StratusRepository<ResultDescription>(dbContext);
                }
                return resultDescriptions;
            }
        }

        public IRepository<Sequence> Sequences
        {
            get
            {
                if (sequences == null)
                {
                    sequences = new StratusRepository<Sequence>(dbContext);
                }
                return sequences;
            }
        }

        public IRepository<SequenceExecution> SequenceExecutions
        {
            get
            {
                if (sequenceExecutions == null)
                {
                    sequenceExecutions = new StratusRepository<SequenceExecution>(dbContext);
                }
                return sequenceExecutions;
            }
        }

        public IRepository<Tester> Testers
        {
            get
            {
                if (testers == null)
                {
                    testers = new StratusRepository<Tester>(dbContext);
                }
                return testers;
            }
        }

        public IRepository<Visit> Visits
        {
            get
            {
                if (visits == null)
                {
                    visits = new StratusRepository<Visit>(dbContext);
                }
                return visits;
            }
        }

        public IRepository<Zone> Zones
        {
            get
            {
                if (zones == null)
                {
                    zones = new StratusRepository<Zone>(dbContext);
                }
                return zones;
            }
        }

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

        public int ExecuteSp(string p, params SqlParameter[] parameters)
        {
            return dbContext.Database.ExecuteSqlCommand(p, parameters);
        }
    }
}

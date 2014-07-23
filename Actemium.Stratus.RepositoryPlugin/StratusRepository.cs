using Actemium.Stratus.Contracts;
using Actemium.Stratus.DataObjects;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Actemium.Stratus.RepositoryPlugin
{

    public class StratusRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected ObjectSet<T> objectSet;
        protected DbContext dbContext;

        public StratusRepository(DbContext dbContext)
        {
            var adapter = ((IObjectContextAdapter)dbContext).ObjectContext;
            this.objectSet = adapter.CreateObjectSet<T>();
            
        }

        public IQueryable<T> FindAll()
        {
            return objectSet;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return objectSet.Where(predicate);
        }

        public T FindById(int id)
        {
            return objectSet.SingleOrDefault(o => o.Id == id);
        }

        public void Add(T newEntity)
        {
            objectSet.AddObject(newEntity);
        }

        public void Remove(T entity)
        {
            objectSet.DeleteObject(entity);
        }
    }
}

using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using WhoSol.Contracts;

namespace WhoSol.EntityFrameworkPlugin
{

    public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected ObjectSet<T> objectSet;
        protected DbContext dbContext;

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

        public object Store
        {
            get { return dbContext; }
            set
            {
                if (value is DbContext)
                {
                    dbContext = value as DbContext;
                    var adapter = ((IObjectContextAdapter)dbContext).ObjectContext;
                    this.objectSet = adapter.CreateObjectSet<T>();
                }
                else
                {
                    throw new ArgumentException("Store is not of type DbContext");
                }
            }
        }
    }
}

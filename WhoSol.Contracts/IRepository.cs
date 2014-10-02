using System;
using System.Linq;
using System.Linq.Expressions;

namespace WhoSol.Contracts
{
    public interface IRepository<T> where T : class
    {
        object Store { get; set; }
        IQueryable<T> FindAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T FindById(int id);
        void Add(T newEntity);
        void Remove(T entity);
    }
}

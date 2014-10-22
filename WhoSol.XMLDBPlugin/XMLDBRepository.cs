using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using WhoSol.Contracts;
using WhoSol.Utilities.Extensions;

namespace WhoSol.XMLDBPlugin
{
    public sealed class XMLDBRepository<T> : IRepository<T> where T : class, IEntity
    {
        private XElement db;
        private readonly List<T> entitySet;
        private ILogger logger;

        public XMLDBRepository(ILogger logger)
        {
            this.logger = logger;
            this.entitySet = new List<T>();
        }

        public IQueryable<T> FindAll()
        {
            return entitySet.AsQueryable<T>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return entitySet.Where(predicate.Compile()).AsQueryable<T>();
        }

        public T FindById(int id)
        {
            return entitySet.Where(o => o.Id == id).SingleOrDefault();
        }

        public void Add(T newEntity)
        {  
            //if there are any entities found get the next ID else it must be the first
            int nextId = entitySet.Any() ? entitySet.Max(o => o.Id) + 1 : 1;

            if (FindById(newEntity.Id) == null)
            {
                if (newEntity.Id == 0)
                {
                    newEntity.Id = nextId;
                }
                if (db != null)
                {
                    db.Add(newEntity.ToXElement<T>());
                    entitySet.Add(newEntity);
                }
                else
                {
                    logger.Error("DB Not initialised");
                }
            }
        }

        public void Remove(T entity)
        {
            entitySet.RemoveAll(o => o.Id == entity.Id);
            var entityToRemove = db.Elements()
                .Where(el => (int)el.Attribute("Id") == entity.Id)
                .SingleOrDefault();

            if (entityToRemove != null)
            {
                entityToRemove.Remove();
            }
        }

        public bool Update(T entity)
        {
            T entityToUpdate = FindById(entity.Id);

            if (entityToUpdate != null)
            {
                Remove(entityToUpdate);
                Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public object Store
        {
            get { return db; }
            set
            {
                if (value is XElement)
                {
                    db = value as XElement;
                    var elements = db.Elements();
                    if (elements.Any())
                    {
                        entitySet.AddRange(from element in elements select element.FromXElement<T>());
                    }
                }
                else
                {
                    throw new ArgumentException("Store is not of type XElement");
                }
            }
        }
    }
}

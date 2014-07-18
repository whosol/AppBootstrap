using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Actemium.Stratus.Utilities
{
    public sealed class UniqueQueue<T> : IDisposable
    {
        #region Fields

        private readonly HashSet<T> uniqueKeys = new HashSet<T>();
        private readonly object syncLock = new object();
        private readonly BlockingCollection<T> innerCollection = new BlockingCollection<T>(); 
        
        #endregion
       
        #region Properties

        public int Count
        {
            get
            {
                return innerCollection.Count();

                //lock (syncLock)
                //{
                //    return innerQueue.Count;
                //}
            }
        }

        #endregion

        #region Public Methods
        
        public void Enqueue(T item)
        {
            if (!uniqueKeys.Contains(item))
            {
                lock (syncLock)
                {
                    uniqueKeys.Add(item);
                }

                innerCollection.Add(item);
            }
        }

        public T Dequeue()
        {

            var item = innerCollection.Take();
            lock (syncLock)
            {
                uniqueKeys.Remove(item);
                return item;
            }
        } 

        #endregion
        
        #region IDisposable Interface

        public void Dispose()
        {
            innerCollection.Dispose();
        } 

        #endregion
    }
}

using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace WhoSol.WebApiPlugin
{
    public class NinjectDependencyScope : IDependencyScope
    {
        private readonly IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver) 
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            //IDisposable disposable = resolver as IDisposable;
            //if (disposable != null)
            //    disposable.Dispose();

            //resolver = null;
        }
    }
}

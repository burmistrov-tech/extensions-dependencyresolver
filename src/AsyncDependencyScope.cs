using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public readonly struct AsyncDependencyScope : IDependencyScope, IAsyncDisposable
    {
        private readonly IDependencyScope _dependencyScope;
        
        public AsyncDependencyScope(IDependencyScope serviceScope)
        {
            _dependencyScope = serviceScope ?? throw new ArgumentNullException(nameof(serviceScope));
        }
        
        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return _dependencyScope.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return _dependencyScope.GetServices(serviceType);
        }
        
        public void Dispose()
        {
            _dependencyScope.Dispose();
        }
        
        public ValueTask DisposeAsync()
        {
            if (_dependencyScope is IAsyncDisposable ad)
            {
                return ad.DisposeAsync();
            }
           
            _dependencyScope.Dispose();
            
            return default;
        }
    }
}
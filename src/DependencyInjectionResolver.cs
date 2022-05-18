using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public class DependencyInjectionResolver : IDependencyInjectionResolver
    {
        internal readonly IServiceProvider ServiceProvider;
        private bool _disposed;

        public DependencyInjectionResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IDependencyScope BeginScope()
        {
            var scope = ServiceProvider.CreateScope();

            return new DependencyInjectionResolver(scope.ServiceProvider);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return ServiceProvider.GetServices(serviceType);
        }

        public void Dispose()
        {
            if (_disposed || !(ServiceProvider is IDisposable disposable)) return;
            
            _disposed = true;
            disposable.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            if (ServiceProvider is IAsyncDisposable ad)
            {
                _disposed = true;
                return ad.DisposeAsync();
            }
            
            Dispose();
            
            return default;
        }
    }
}
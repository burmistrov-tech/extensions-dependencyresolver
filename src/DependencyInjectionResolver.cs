using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public class DependencyInjectionResolver : IDependencyInjectionResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;

        public DependencyInjectionResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IDependencyScope BeginScope()
        {
            var scope = _serviceProvider.CreateScope();

            return new DependencyInjectionResolver(scope.ServiceProvider);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return _serviceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            
            return _serviceProvider.GetServices(serviceType);
        }

        public void Dispose()
        {
            if (_disposed || !(_serviceProvider is IDisposable disposable)) return;
            
            _disposed = true;
            disposable.Dispose();
        }

        public ValueTask DisposeAsync()
        {
            if (_serviceProvider is IAsyncDisposable ad)
            {
                _disposed = true;
                return ad.DisposeAsync();
            }
            
            Dispose();
            
            return default;
        }
    }
}
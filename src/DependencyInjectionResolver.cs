using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public class DependencyInjectionResolver : IDependencyResolver, IServiceProvider
    {
        internal readonly IServiceProvider ServiceProvider;
        private bool _disposed;

        public DependencyInjectionResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IDependencyScope BeginScope()
        {
            var scope = ServiceProvider.CreateScope();

            return new DependencyInjectionResolver(scope.ServiceProvider);
        }

        public object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType);
        }

        public void Dispose()
        {
            if (!_disposed && ServiceProvider is IDisposable disposable)
            {
                _disposed = true;
                disposable.Dispose();
            }
        }
    }
}
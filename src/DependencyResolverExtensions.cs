using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public static class DependencyResolverExtensions
    {
        public static T GetService<T>(this IDependencyScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));

            return (T) scope.GetService(typeof(T));
        }

        public static IEnumerable<T> GetServices<T>(this IDependencyScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));

            return (IEnumerable<T>) scope.GetServices(typeof(T));
        }

        public static object GetRequiredService(this IDependencyScope scope, Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            return scope.GetService(serviceType) ??
                   throw new InvalidOperationException(
                       "Unable to resolve service for type while attempting to activate");
        }

        public static T GetRequiredService<T>(this IDependencyScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));

            return (T) scope.GetRequiredService(typeof(T));
        }

        public static AsyncDependencyScope BeginAsyncScope(this IDependencyResolver resolver)
        {
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));

            var scope = resolver.BeginScope();

            return new AsyncDependencyScope(scope);
        }
    }
}
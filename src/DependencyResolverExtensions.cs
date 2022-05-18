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

            return scope.GetService(serviceType) ?? throw new InvalidOperationException("");
        }

        public static T GetRequiredService<T>(this IDependencyScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));

            return (T) scope.GetRequiredService(typeof(T));
        }
    }
}
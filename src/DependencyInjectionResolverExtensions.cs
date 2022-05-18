using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public static class DependencyInjectionResolverExtensions
    {
        public static T GetService<T>(this DependencyInjectionResolver resolver)
        {
            return resolver.ServiceProvider.GetService<T>();
        }

        public static IEnumerable<T> GetServices<T>(this DependencyInjectionResolver resolver)
        {
            return resolver.ServiceProvider.GetServices<T>();
        }

        public static object GetRequiredService(this DependencyInjectionResolver resolver, Type serviceType)
        {
            return resolver.ServiceProvider.GetRequiredService(serviceType);
        }

        public static T GetRequiredService<T>(this DependencyInjectionResolver resolver)
        {
            return resolver.ServiceProvider.GetRequiredService<T>();
        }
    }
}
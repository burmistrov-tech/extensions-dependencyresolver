using System;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var controllers = Assembly.GetExecutingAssembly().ExportedTypes
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IHttpController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));

            foreach (var controller in controllers)
            {
                services.AddTransient(controller);
            }

            return services;
        }

        public static IDependencyResolver ToDependencyResolver(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            
            var serviceProvider = services.BuildServiceProvider();
            
            return new DependencyInjectionResolver(serviceProvider);
        }
    }
}
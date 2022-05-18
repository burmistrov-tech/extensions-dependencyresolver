using System;
using System.Web.Http.Dependencies;

namespace BurmistrovTech.Extensions.DependencyResolver
{
    public interface IDependencyInjectionResolver: IDependencyResolver, IServiceProvider, IAsyncDisposable { }
}
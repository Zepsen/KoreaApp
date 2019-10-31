using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Korea.Infrastructure.Extensions
{
    public static class ServiceExts
    {
        public static void RegisterAllTypes(this IServiceCollection services,
            Type t,
            Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            foreach (var type in assemblies.SelectMany(i => i.DefinedTypes).Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == t)
                    {                        
                        var interfaceType = t.MakeGenericType(i.GetGenericArguments());
                        services.AddTransient(interfaceType, type);
                        services.Add(new ServiceDescriptor(interfaceType, type, lifetime));
                    }
                }
            }
        }
              

        public static void RegisterAllTypes<T>(this IServiceCollection services, 
            Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(T))));
            foreach (var type in typesFromAssemblies)
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
        }
    }
}

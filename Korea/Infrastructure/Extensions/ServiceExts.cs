﻿using Microsoft.Extensions.DependencyInjection;
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
            var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes
                    .Where(x => x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == t))).ToList();
                        
            foreach (var type in typesFromAssemblies)
            {
                var inType = type.ImplementedInterfaces.FirstOrDefault().GetGenericArguments().FirstOrDefault();
                //GenericFactory.Register(inType, type);
                var genT = t.MakeGenericType(inType);
                services.Add(new ServiceDescriptor(genT, type, lifetime));
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

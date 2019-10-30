using System;
using System.Collections.Generic;

namespace Handlers.Core
{   
    /// <summary>
    /// Do not delete, example
    /// First register in services by Interface<T> 
    /// </summary>
    public static class GenericFactory
    {
        private static Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();

        public static void Register(Type t, Type t2)
        {
            registeredTypes.Add(t, t2);
        }

        public static IAuthorizationConfig<T> CreateGeneric<T>()
        {
            var t = typeof(T);
            if (registeredTypes.ContainsKey(t) == false) throw new NotSupportedException();

            var typeToCreate = registeredTypes[t];
            return Activator.CreateInstance(typeToCreate, true) as IAuthorizationConfig<T>;
        }

    }

    public interface IAuthorizationConfig<T>
    {       
        bool AllowAnonymous();
    }

    
}

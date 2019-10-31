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
        bool Allow();
        void Check(BaseRequest request);
        void Check(IBaseRequest<T> request);
    }

    public abstract class AbstractAuthorization<T> : IAuthorizationConfig<T>
    {
        private bool _allowAnonymous = false;
        private List<string> _roles = new List<string>();

        protected void AllowAnonymous() => _allowAnonymous = true;
        protected void AddRole(string role) => _roles.Add(role);
        protected void AddRoles(List<string> roles) => _roles.AddRange(roles);

        public bool Allow() => _allowAnonymous;
        
        public bool IsInRole(string role)
        {
            return _roles.Contains(role);
        }

        public void Check(BaseRequest request)
        {
            var a = request?.Token;
        }
        public void Check(IBaseRequest<T> request)
        {
            var a = request?.Token;
        }

    }
}

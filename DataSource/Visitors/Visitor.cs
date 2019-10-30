using System;
using System.Collections.Generic;
using static Handlers.CategoryAllQuery;

namespace Handlers.Visitors
{
    public interface IVisitor
    {
        bool Allow(IVisitor visitor);
    }
    public class VisitorBorker
    {
        private readonly IVisitor visitor;

        public VisitorBorker(IVisitor visitor)
        {
            this.visitor = visitor;
        }

        public bool Visit()
        {
            return this.visitor.Allow(visitor);
        }
    }


    public interface IVisitor<T>
    {
        bool Allow(T visitor);
    }

    public static class GenericFactory
    {
        private static Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>();

        public static void Register(Type t, Type t2)
        {
            registeredTypes.Add(t, t2);
        }

        public static IGeneric<T> CreateGeneric<T>()
        {
            var t = typeof(T);
            if (registeredTypes.ContainsKey(t) == false) throw new NotSupportedException();

            var typeToCreate = registeredTypes[t];
            return Activator.CreateInstance(typeToCreate, true) as IGeneric<T>;
        }

    }

    public interface IGeneric<T>
    {       
        void Process();
    }

    
}

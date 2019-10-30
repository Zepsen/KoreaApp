namespace Handlers.Visitors
{
    public interface IVisitor
    {
        bool Allow();
    }

    public interface IVisitor<T>
    {
        bool Allow();
    }

    public abstract class BaseVisitor<T> : IVisitor<T>
    {
        public abstract bool Allow();        
    }
}

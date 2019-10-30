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

    //public class BaseVisitor<T> : IVisitor<T>
    //{
    //    protected bool AllowAnonymous { get; set; } = false;
        
    //    public bool Allow()
    //    {
    //        return this.AllowAnonymous;
    //    }
    //}
}

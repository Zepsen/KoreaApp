using MediatR;

namespace Handlers
{
    public interface IBaseRequest<Request> : IRequest<Request>
    {
        string Token { get; set; }
    }

    public abstract class BaseRequest<Request> : IBaseRequest<Request>
    {
        public string Token { get; set; }
    }
}

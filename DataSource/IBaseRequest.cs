using MediatR;

namespace Handlers
{
    public interface IBaseRequest<Request> : IRequest<Request>
    {
        string Token { get; set; }
    }

    public abstract class BaseRequest
    {
        public string Token { get; set; }
    }
}

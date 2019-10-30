using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Visitors;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        //IBaseRequest<TResponse>
    {
        private readonly IVisitor _service;

        public AuthBehavior(IVisitor service)
        {
            this._service = service;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
        )
        {
            try
            {
                //this.service.Validate(request.Token);
                _service.Allow();

                return next();
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

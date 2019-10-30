using Handlers.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest<TResponse>
    {
        private readonly IAuthService service;

        public AuthBehavior(IAuthService service)
        {
            this.service = service;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
        )
        {
            try
            {
                //this.service.Validate(request.Token);
                return next();
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

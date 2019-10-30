using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Core;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>        
    {
        private readonly IAuthorizationConfig<TRequest> generic;

        public AuthBehavior(IAuthorizationConfig<TRequest> generic)
        {
            this.generic = generic;
        }


        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
        )
        {
            try
            {
                //var generic = GenericFactory.CreateGeneric<TRequest>();
                //generic.Process();
                if (generic.AllowAnonymous())
                {
                    return next();
                }
                else throw new System.Exception("Not allow");
                
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

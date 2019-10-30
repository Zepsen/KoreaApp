using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Visitors;
using System.Collections.Generic;
using System.Linq;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>        
    {
        private readonly IGeneric<TRequest> generic;

        public AuthBehavior(IGeneric<TRequest> generic)
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
                generic.Process();

                return next();
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

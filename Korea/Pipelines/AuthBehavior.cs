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
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
        )
        {
            try
            {
                var gen = GenericFactory.CreateGeneric<TRequest>();
                gen.Process();


                return next();
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

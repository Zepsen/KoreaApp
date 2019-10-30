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
        //IBaseRequest<TResponse>
    {
        //private readonly IVisitor<TRequest> _service;
        private readonly IEnumerable<IVisitor<TRequest>> _services;

        public AuthBehavior(IEnumerable<IVisitor<TRequest>> visitors)
        {
            //this._service = service;
            this._services = visitors;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next
        )
        {
            try
            {
                //this.service.Validate(request.Token);
                //_service.Allow();
                var a = _services.FirstOrDefault();
                
                return next();
            } catch (System.Exception)
            {
                throw;
            }
            
            
        }
    }
}

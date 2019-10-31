using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Core;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>        
    {
        private readonly IEnumerable<IAuthorizationConfig<TRequest>> _auth;

        public AuthBehavior(IEnumerable<IAuthorizationConfig<TRequest>> auth)
        {
            _auth = auth;
        }


        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {                   
                if (_auth.FirstOrDefault()?.AllowAnonymous() ?? true)
                {
                    return next();
                }
                else throw new Exception("Not allow");
                
            } catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}

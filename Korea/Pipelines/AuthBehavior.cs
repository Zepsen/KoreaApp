using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Core;
using System.Collections.Generic;
using System.Linq;
using System;
using Handlers.Services;
using Handlers;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IAuthorizationConfig<TRequest> _auth;
        private readonly IAuthService authService;

        public AuthBehavior(
            IEnumerable<IAuthorizationConfig<TRequest>> auth,
            IAuthService authService    
        )
        {
            _auth = auth.FirstOrDefault();
            this.authService = authService;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                //Do not need auth
                
                if (_auth?.Allow() ?? true) return await next();
                else
                {
                    var token = authService.Validate((request as BaseRequest).Token);
                    if (true)
                    {                        
                        return await next();
                    } else throw new Exception("You can't reach this");

                }

            } catch (Exception ex)
            {
                throw;
            }
        }
    }
}

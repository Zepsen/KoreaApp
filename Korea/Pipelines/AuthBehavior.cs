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
        where TRequest : IBaseRequest<TResponse>     
    {
        private readonly IAuthorizationConfig<TRequest> _auth;
        private readonly IAuthService _authService;

        public AuthBehavior(IEnumerable<IAuthorizationConfig<TRequest>> auth, IAuthService authService)
        {
            _auth = auth.FirstOrDefault();
            _authService = authService;
        }


        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                //Do not need auth
                if (_auth?.Allow() ?? true) return next();
                else 
                {
                    var token = _authService.Validate(request.Token);
                    throw new Exception("Not allow");                                       
                }
                
            } catch (Exception ex)
            {
                throw;
            }            
        }
    }
}

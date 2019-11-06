using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Core;
using System.Collections.Generic;
using System.Linq;
using System;
using Handlers.Services;
using Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>     
    {
        private readonly IAuthorizationConfig<TRequest> _auth;
        private readonly AuthenticationStateProvider _authService;

        public AuthBehavior(IEnumerable<IAuthorizationConfig<TRequest>> auth, AuthenticationStateProvider authService)
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
                    var user = _authService.GetAuthenticationStateAsync().Result;
                    var role = user.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role).Value;
                    if (_auth.IsInRole(role))
                    {                        
                        return next();
                    } else throw new Exception("You can't reach this");
                }
                
            } catch (Exception ex)
            {
                throw;
            }            
        }
    }
}

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Handlers.Core;
using System.Collections.Generic;
using System.Linq;
using System;
using Handlers.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Korea.Pipelines
{
    public class AuthBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>     
    {
        private readonly IAuthorizationConfig<TRequest> _auth;
        private readonly IAuthService _authService;
        private readonly AuthenticationStateProvider _authProvider;

        public AuthBehavior(IEnumerable<IAuthorizationConfig<TRequest>> auth, 
            IAuthService authService)
            //AuthenticationStateProvider authService)
        {
            _auth = auth.FirstOrDefault();
            _authService = authService;
        }


        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                //Do not need auth
                if (_auth?.Allow() ?? true) return await next();
                else 
                {
                    //By custom provider
                    var user = await _authProvider.GetAuthenticationStateAsync();
                    
                    var role = user.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role).Value;
                    if (_auth.IsInRole(role))                    
                    {
                        return await next();
                    } else throw new Exception("You can't reach this");

                    //By token
                    //if(true)
                    //{
                    //    _auth.Check(request as BaseRequest);

                    //By cookie
                    //await _authService.LogoutForCookie();
                    //await _authService.LoginForCookie("test@test", "nametest");
                    //if (_authService.IsAuth()) {                        
                    //    return await next();
                    //} else throw new Exception("You can't reach this");
                }
                
            } catch (Exception ex)
            {
                throw;
            }            
        }
    }
}

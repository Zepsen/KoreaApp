using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Handlers.Services;
using MediatR;

namespace Handlers
{
    public class UserLoginQuery
    {
        public class Request : IRequest<User>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class User
        {            
            public string Token { get; set; }            
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {                
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
            }
        }

        public class UserQueryHandler : IRequestHandler<Request, User>
        {
            private readonly IAuthService authService;

            public UserQueryHandler(IAuthService authService)
            {
                this.authService = authService;
            }

            public async Task<User> Handle(Request request, CancellationToken cancellationToken)
            {
                var token = authService.GenerateToken(request.Email);
                return new User
                {
                    Token = token
                };                
            }
        }
    }
}

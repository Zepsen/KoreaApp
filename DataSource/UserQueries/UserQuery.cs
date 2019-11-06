using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DataSource.Handlers
{
    public class UserQuery
    {
        public class Request : IRequest<User>
        {
            public int Id { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
        }

        public class RequestValidator : AbstractValidator<Request>
        {
            public RequestValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
            }
        }

        public class UserQueryHandler : IRequestHandler<Request, User>
        {
            public async Task<User> Handle(Request request, CancellationToken cancellationToken)
            {
                return new User
                {
                    Id = 1,
                    Name = "Test",
                    Email = "test@test.com",
                    Age = 21
                };
            }
        }
    }
}

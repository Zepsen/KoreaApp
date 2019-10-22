using System.Threading;
using System.Threading.Tasks;
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
            public int Age { get; set; }
        }

        public class UserQueryHandler : IRequestHandler<Request, User>
        {
            public async Task<User> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

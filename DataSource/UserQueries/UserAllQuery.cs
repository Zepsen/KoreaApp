using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Handlers
{
    public class UserAllQuery
    {
        public class Request : IRequest<List<User>>
        {
        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class UserAllHandler : Query, IRequestHandler<Request, List<User>>
        {
            public async Task<List<User>> Handle(Request request, CancellationToken cancellationToken)
            {
                return await QueryAsync<User>("spUsers_SelectAll", null);                
            }
        }
    }
}

using MediatR;
using System.Collections.Generic;

namespace DataSource.Data
{
    public class UserRequest : IRequest<List<User>>
    {        
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
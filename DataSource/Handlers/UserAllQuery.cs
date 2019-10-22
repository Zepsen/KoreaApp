using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataSource.Data
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

        public class UserAllHandler
            : IRequestHandler<Request, List<User>>
        {
            private readonly string _connectionString = "Data Source=EPUAKYIW3729;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            public async Task<List<User>> Handle(Request request, CancellationToken cancellationToken)
            {
                IEnumerable<User> users;
                using (IDbConnection conn = new SqlConnection(_connectionString))
                {
                    users = await conn.QueryAsync<User>("spUsers_SelectAll", null, commandType: CommandType.StoredProcedure);
                }

                return users.ToList();
            }
        }
    }
}

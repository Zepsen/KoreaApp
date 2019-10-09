using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace DataSource
{
    public class DbContext : IDbContext
    {
        private readonly string _connectionString = "Data Source=EPUAKYIW3729;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task GetAsync()
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                var users = await conn.QueryAsync<User>("spUsers_SelectAll", null, commandType: CommandType.StoredProcedure);
            }
        }
    }

    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

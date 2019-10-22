using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Handlers
{
    public abstract class Query
    {
        protected readonly string ConnectionString 
            = "Data Source=EPUAKYIW3729;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected async Task<List<T>> QueryAsync<T>(string sql, object request)
        {
            IEnumerable<T> res;
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                res = await conn.QueryAsync<T>(sql, request, commandType: CommandType.StoredProcedure);
            }

            return res.ToList();
        }
    }
}

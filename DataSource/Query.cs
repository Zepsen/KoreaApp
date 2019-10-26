using Dapper;
using SharedModels;
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
            = "Data Source=DESKTOP-3OG4GO9;Initial Catalog=TestDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected async Task<List<T>> QueryAsync<T>(string sql, object request)
        {
            IEnumerable<T> res;
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                res = await conn.QueryAsync<T>(sql, request, commandType: CommandType.StoredProcedure);
            }

            return res.ToList();
        }
                

        protected async Task<Result<T>> QueryMultipleAsync<T>(string sql, object request)
        {
            Result<T> res = new Result<T>();
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                var grid = await conn.QueryMultipleAsync(sql, request, commandType: CommandType.StoredProcedure);
                res.Data = grid.Read<T>().ToList();
                res.Total = grid.Read<int>().First();
            }

            return res;
        }
    }
}

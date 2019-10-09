using System.Threading.Tasks;

namespace DataSource
{
    public interface IDbContext
    {
        Task GetAsync();
    }
}
using System.Threading.Tasks;
using TvMazeScrapper.DataAccess.App;

namespace TvMazeScrapper.DataAccess
{
    public interface IRepository<T> where T : class
    {
        Task SaveAsync(T model);
        AsyncQuery<T> QueryAsync();
    }
}
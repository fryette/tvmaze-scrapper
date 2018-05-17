using System.Threading.Tasks;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.Providers
{
    public interface ITvMazeShowProvider
    {
        Task<PageModel> LoadTvMazePageAsync(int pageNumber);
    }
}
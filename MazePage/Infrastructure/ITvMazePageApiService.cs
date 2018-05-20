using System.Threading.Tasks;
using MazePage.DataModels;

namespace MazePage.Infrastructure
{
    public interface ITvMazePageApiService
    {
        Task<MazePageData> FetchShowsAsync(int page);
    }
}

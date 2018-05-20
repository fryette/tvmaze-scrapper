using System.Threading.Tasks;
using MazePage.DataModels;

namespace MazePage.Infrastructure
{
    public interface IMazePageClient
    {
        Task<MazePageData> FetchShowsAsync(int page);
    }
}

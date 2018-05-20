using System.Threading.Tasks;
using MazePage.DataModels;

namespace MazePage.Infrastructure
{
    public interface IMazePageStore
    {
        Task SaveMazePageAsync(MazePageData page);
        Task<MazePageData> GetMazePageAsync(int pageId);
    }
}

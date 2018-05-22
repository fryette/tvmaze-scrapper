using System.Collections.Generic;
using System.Threading.Tasks;
using MazePage.DataModels;

namespace MazePage.Infrastructure
{
    public interface IMazePageStore
    {
        Task SaveMazePageAsync(int pageId,IEnumerable<Show> shows);
        Task<List<Show>> LoadShowsByPageIdAsync(int pageId);
    }
}

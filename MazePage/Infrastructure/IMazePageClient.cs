using System.Collections.Generic;
using System.Threading.Tasks;
using MazePage.DataModels;

namespace MazePage.Infrastructure
{
    public interface IMazePageClient
    {
        Task<IEnumerable<Show>> FetchShowsAsync(int page);
    }
}

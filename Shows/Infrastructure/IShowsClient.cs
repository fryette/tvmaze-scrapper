using System.Collections.Generic;
using System.Threading.Tasks;
using Shows.DataModels;

namespace Shows.Infrastructure
{
    public interface IShowsClient
    {
        Task<IReadOnlyList<Show>> FetchShowsAsync(int pageNumber, int from, int to);
    }
}
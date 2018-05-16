using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.Services.Api.TvMazeApi
{
    public interface ITvMazeApiService
    {
        Task<IEnumerable<ShowModel>> FetchShowsAsync(int page);
        Task<List<PersonInfo>> FetchCastByShowIdAsync(string showId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Services.Api.TvMazeApi
{
    public interface ITvMazeApiService
    {
        Task<IEnumerable<ShowModel>> FetchShowsAsync(int page);
        Task<List<PersonModel>> FetchCastByShowIdAsync(string showId);
    }
}

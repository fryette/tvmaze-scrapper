using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Services.Api.TvMazeApi
{
    public interface ITvMazeApiService
    {
        Task<PageModel> FetchShowsAsync(int page);
        Task<List<PersonModel>> FetchCastByShowIdAsync(string showId);
    }
}

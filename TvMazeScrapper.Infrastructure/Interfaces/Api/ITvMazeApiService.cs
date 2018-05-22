using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.Api
{
    public interface ITvMazeApiService
    {
        Task<PageModel> FetchShowsAsync(int page);
        Task<List<PersonModel>> FetchCastByShowIdAsync(string showId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.Api
{
    public interface IScrapperApiService
    {
        Task<List<ShowModel>> LoadShowsAsync(int pageNumber = 0);
    }
}

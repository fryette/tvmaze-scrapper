using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.Services.Api
{
    public interface IScrapperApiService
    {
        Task<List<ShowModel>> LoadShowsAsync(int pageNumber = 0);
    }
}

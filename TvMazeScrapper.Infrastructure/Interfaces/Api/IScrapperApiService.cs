using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.Api
{
    public interface IScrapperApiService
    {
        Task<IEnumerable<ShowModel>> LoadShowsAsync(int pageNumber = 0);
    }
}

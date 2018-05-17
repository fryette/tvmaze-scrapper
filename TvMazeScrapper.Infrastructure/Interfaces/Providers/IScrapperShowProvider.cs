using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Infrastructure.Interfaces.Providers
{
    public interface IScrapperShowProvider
    {
        Task<IReadOnlyList<ShowModel>> FillCastForShowsAsync(IReadOnlyList<ShowModel> shows);
        Task SavePageAsync(PageModel page);
        Task<PageModel> TryGetPageAsync(int pageNumber);
    }
}
using System.Threading.Tasks;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.Infrastructure.Interfaces
{
    public interface IPageRepository
    {
        Task<Page> TryGetPageAsync(int pageNumber);
        Task SavePageAsync(Page page);
    }
}

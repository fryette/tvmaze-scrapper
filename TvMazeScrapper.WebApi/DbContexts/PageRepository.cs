using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Infrastructure.Interfaces;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.WebApi.DbContexts
{
    public class PageRepository : IPageRepository
    {
        private readonly PageContext _dbContext;

        public PageRepository(PageContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Page> TryGetPageAsync(int pageNumber)
        {
            return await _dbContext.Pages.FirstOrDefaultAsync(x => x.Id == pageNumber);
        }

        public async Task SavePageAsync(Page page)
        {
            await _dbContext.Pages.AddAsync(page);
            await _dbContext.SaveChangesAsync();
        }
    }
}

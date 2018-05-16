using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TvMazeScrapper.Domain;
using TvMazeScrapper.Infrastructure.Interfaces;
using TvMazeScrapper.Infrastructure.Interfaces.App;
using TvMazeScrapper.Infrastructure.Interfaces.DataServices;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.WebApi.DbContexts
{
    public class PageRepository : IPageRepository
    {
        private readonly PageContext _dbContext;
        private readonly IMapper _mapper;

        public PageRepository(PageContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PageModel> TryGetPageAsync(int pageNumber)
        {
            return _mapper.Map<PageModel>(await _dbContext.Pages.FirstOrDefaultAsync(x => x.Id == pageNumber));
        }

        public async Task SavePageAsync(PageModel pageModel)
        {
            await _dbContext.Pages.AddAsync(_mapper.Map<Page>(pageModel));
            await _dbContext.SaveChangesAsync();
        }
    }
}

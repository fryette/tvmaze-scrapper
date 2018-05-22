using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.DataAccess;
using TvMazeScrapper.Domain.App;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Infrastructure.Interfaces.App;
using TvMazeScrapper.Infrastructure.Interfaces.Providers;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Services.Providers
{
    public class ScrapperShowProvider : IScrapperShowProvider
    {
        private readonly IMapper _mapper;
        private readonly IRepository<AppPage> _repository;
        private readonly ITvMazeApiService _tvMazeApiService;

        public ScrapperShowProvider(IRepository<AppPage> repository, IMapper mapper, ITvMazeApiService tvMazeApiService)
        {
            _tvMazeApiService = tvMazeApiService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageModel> TryGetPageAsync(int pageNumber)
        {
            return _mapper.Map<PageModel>(await _repository.QueryAsync().FirstOrDefaultAsync(x => x.Id == pageNumber));
        }

        public Task SavePageAsync(PageModel page)
        {
            return _repository.SaveAsync(_mapper.Map<AppPage>(page));
        }

        public async Task<IReadOnlyList<ShowModel>> FillCastForShowsAsync(IReadOnlyList<ShowModel> shows)
        {
            foreach (var showModel in shows)
            {
                var cast = await _tvMazeApiService.FetchCastByShowIdAsync(showModel.Id);
                showModel.Cast = new List<PersonModel>(cast.OrderBy(x => x.Birthday));
            }

            return shows;
        }
    }
}
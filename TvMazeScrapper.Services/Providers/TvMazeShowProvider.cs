using System;
using System.Threading.Tasks;
using TvMazeScrapper.DataAccess;
using TvMazeScrapper.Domain.TvMaze;
using TvMazeScrapper.Infrastructure.Interfaces.App;
using TvMazeScrapper.Infrastructure.Interfaces.Providers;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi;

namespace TvMazeScrapper.Services.Providers
{
    public class TvMazeShowProvider : ITvMazeShowProvider
    {
        private const int ITEMS_PER_PAGE = 10;
        private const int TV_MAZE_ITEMS_PER_PAGE = 250;
        private readonly ITvMazeApiService _apiService;

        private readonly IMapper _mapper;
        private readonly IRepository<TvMazePage> _repository;

        public TvMazeShowProvider(IMapper mapper, ITvMazeApiService apiService, IRepository<TvMazePage> repository)
        {
            _mapper = mapper;
            _apiService = apiService;
            _repository = repository;
        }

        public async Task<PageModel> LoadTvMazePageAsync(int pageNumber)
        {
            var tvMazePageNumber = GetTvMazePage(pageNumber);

            var cachedTvMazePage = await _repository.QueryAsync().FirstOrDefaultAsync(x => x.Id == tvMazePageNumber);

            if (cachedTvMazePage != null)
                return _mapper.Map<PageModel>(cachedTvMazePage);

            return await _apiService.FetchShowsAsync(tvMazePageNumber);
        }

        private int GetTvMazePage(int requestedPage)
        {
            return (int) Math.Floor((requestedPage + 1.0) * ITEMS_PER_PAGE / TV_MAZE_ITEMS_PER_PAGE);
        }
    }
}
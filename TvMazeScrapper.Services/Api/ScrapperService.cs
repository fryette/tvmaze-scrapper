using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Infrastructure.Interfaces.Providers;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.Services.Api
{
    public class ScrapperService : IScrapperApiService
    {
        private const int ITEMS_PER_PAGE = 10;
        private const int TV_MAZE_ITEMS_PER_PAGE = 250;
        private readonly IScrapperShowProvider _showsProvider;

        private readonly ITvMazeApiService _tvMazeApiService;
        private readonly ITvMazeShowProvider _tvMazeShowsProvider;

        public ScrapperService(
            ITvMazeApiService tvMazeApiService,
            ITvMazeShowProvider tvMazeShowsProvider,
            IScrapperShowProvider showsProvider)
        {
            _tvMazeApiService = tvMazeApiService;
            _tvMazeShowsProvider = tvMazeShowsProvider;
            _showsProvider = showsProvider;
        }

        public async Task<IEnumerable<ShowModel>> LoadShowsAsync(int pageNumber = 0)
        {
            var page = await _showsProvider.TryGetPageAsync(pageNumber);

            if (page != null)
                return page.Shows;

            var showsResult = (await _tvMazeShowsProvider.LoadTvMazePageAsync(pageNumber)).Shows
                .Skip(GetNumberOfItemsThatShouldBeSkipped(pageNumber)).Take(ITEMS_PER_PAGE).ToList();

            var shows = await _showsProvider.FillCastForShowsAsync(showsResult);

            await _showsProvider.SavePageAsync(new PageModel {Id = pageNumber, Shows = shows});

            return shows;
        }

        private int GetNumberOfItemsThatShouldBeSkipped(int pageNumber)
        {
            return pageNumber % (TV_MAZE_ITEMS_PER_PAGE / ITEMS_PER_PAGE) * ITEMS_PER_PAGE;
        }
    }
}
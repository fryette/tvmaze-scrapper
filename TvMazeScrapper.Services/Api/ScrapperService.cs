using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Infrastructure.Interfaces.DataServices;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi;

namespace TvMazeScrapper.Services.Api
{
    public class ScrapperService : IScrapperApiService
    {
        private const int ITEMS_PER_PAGE = 10;
        private const int TV_MAZE_ITEMS_PER_PAGE = 250;

        private readonly ITvMazeApiService _tvMazeApiService;
        private readonly IPageRepository _pageRepository;

        public ScrapperService(
            ITvMazeApiService tvMazeApiService,
            IPageRepository pageRepository)
        {
            _tvMazeApiService = tvMazeApiService;
            _pageRepository = pageRepository;
        }

        public async Task<IEnumerable<ShowModel>> LoadShowsAsync(int pageNumber = 0)
        {
            var page = await _pageRepository.TryGetPageAsync(pageNumber);

            if (page != null)
            {
                return page.Shows;
            }

            var shows = await MapCastToShowAsync(await LoadSowsAsync(pageNumber));

            await _pageRepository.SavePageAsync(new PageModel
            {
                Id = pageNumber,
                Shows = shows
            });

            return shows;
        }

        private async Task<IReadOnlyList<ShowModel>> MapCastToShowAsync(IReadOnlyList<ShowModel> shows)
        {
            foreach (var showModel in shows)
            {
                var cast = await _tvMazeApiService.FetchCastByShowIdAsync(showModel.Id);
                showModel.Cast = new List<PersonModel>(cast.OrderBy(x => x.Birthday));
            }

            return shows;
        }

        private async Task<IReadOnlyList<ShowModel>> LoadSowsAsync(int pageNumber)
        {
            var tvMazePage = GetTvMazePage(pageNumber);

            var cachedTvMazePage = await _pageRepository.TryGetTvMazePageAsync(tvMazePage);

            var shows = cachedTvMazePage != null ? cachedTvMazePage.Shows : await _tvMazeApiService.FetchShowsAsync(tvMazePage);

            return shows.Skip(GetNumberOfItemsThatShouldBeSkipped(pageNumber)).Take(ITEMS_PER_PAGE).ToList();
        }

        private int GetNumberOfItemsThatShouldBeSkipped(int pageNumber)
        {
            return pageNumber % (TV_MAZE_ITEMS_PER_PAGE / ITEMS_PER_PAGE) * ITEMS_PER_PAGE;
        }

        private int GetTvMazePage(int requestedPage)
        {
            return (int)Math.Floor((requestedPage + 1.0) * ITEMS_PER_PAGE / TV_MAZE_ITEMS_PER_PAGE);
        }
    }
}

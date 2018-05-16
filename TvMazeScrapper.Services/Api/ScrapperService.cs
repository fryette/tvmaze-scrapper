using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.Infrastructure.Interfaces;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Infrastructure.Interfaces.DataServices;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi;
using ShowData = TvMazeScrapper.Services.Api.DataModels.ShowData;

namespace TvMazeScrapper.Services.Api
{
    public class ScrapperService : IScrapperApiService
    {
        private const int ITEMS_PER_PAGE = 10;
        private const int TV_MAZE_ITEMS_PER_PAGE = 250;

        private readonly ITvMazeApiService _tvMazeApiService;
        private readonly IPageRepository _pageRepository;

        public ScrapperService(ITvMazeApiService tvMazeApiService, IPageRepository pageRepository)
        {
            _tvMazeApiService = tvMazeApiService;
            _pageRepository = pageRepository;
        }

        public async Task<List<ShowModel>> LoadShowsAsync(int pageNumber = 0)
        {
            var page = await _pageRepository.TryGetPageAsync(pageNumber);

            if (page != null)
            {
                return page.Shows;
            }

            var tvMazePage = GetTvMazePage(pageNumber);

            var shows = (await _tvMazeApiService.FetchShowsAsync(tvMazePage)).Skip(GetItemsThatShouldBeSkipped(pageNumber)).Take(ITEMS_PER_PAGE).ToList();

            foreach (var showModel in shows)
            {
                var cast = await _tvMazeApiService.FetchCastByShowIdAsync(showModel.Id);
                showModel.Cast = new List<PersonModel>(cast.OrderBy(x => x.Birthday));
            }

            await _pageRepository.SavePageAsync(new PageModel(pageNumber)
            {
                Shows = shows.ToList()
            });

            return shows;
        }

        private int GetItemsThatShouldBeSkipped(int pageNumber)
        {
            return pageNumber % (TV_MAZE_ITEMS_PER_PAGE / ITEMS_PER_PAGE) * ITEMS_PER_PAGE;
        }

        private int GetTvMazePage(int requestedPage)
        {
            return (int)Math.Floor((requestedPage + 1.0) * ITEMS_PER_PAGE / TV_MAZE_ITEMS_PER_PAGE);
        }
    }
}

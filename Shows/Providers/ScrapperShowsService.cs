using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shows.DataModels;
using Shows.Infrastructure;

namespace Shows.Providers
{
    public class ScrapperShowsService : IScrapperShowsService
    {
        private readonly IShowsClient _showsClient;
        private readonly ICastClient _castClient;
        private const int ITEMS_PER_PAGE = 10;
        private const int TV_MAZE_ITEMS_PER_PAGE = 250;

        public ScrapperShowsService(IShowsClient showsClient, ICastClient castClient)
        {
            _showsClient = showsClient;
            _castClient = castClient;
        }

        public async Task<IEnumerable<Show>> LoadShowsAsync(int pageNumber = 0)
        {
            var itemsToSkip = GetNumberOfItemsThatShouldBeSkipped(pageNumber);
            var shows = await _showsClient.FetchShowsAsync(
                GetTvMazePage(pageNumber),
                GetNumberOfItemsThatShouldBeSkipped(pageNumber),
                itemsToSkip + ITEMS_PER_PAGE);

            var casts = (await _castClient.FetchCastByShowIdAsync(shows.Select(x => x.Id))).ToDictionary(
                x => x.ShowId,
                x => x.Persons);

            foreach (var show in shows)
            {
                show.Cast = casts[show.Id].OrderByDescending(x => x.Birthday);
            }

            return shows;
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
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MazePage.DataModels;
using MazePage.Infrastructure;
using Newtonsoft.Json;
using Polly;

namespace MazePage.Services
{
    public class TvMazePageService : ITvMazePageApiService
    {
        private const string SHOWS_API_ENDPOINT = "http://api.tvmaze.com/shows";

        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(SHOWS_API_ENDPOINT) };
        private const string GET_MAZE_PAGE_PATH_TEMPLATE = "?page={0}";

        private static readonly Policy ExponentialRetryPolicy =
            Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                    (ex, _) => Console.WriteLine(ex.ToString()));

        public async Task<MazePageData> FetchShowsAsync(int pageNumber)
        {
            string response = string.Empty;

            await ExponentialRetryPolicy.ExecuteAsync(
                async () =>
                {
                    response = await _client.GetStringAsync(string.Format(GET_MAZE_PAGE_PATH_TEMPLATE, pageNumber)).ConfigureAwait(false);
                });

            var showsData = JsonConvert.DeserializeObject<List<ShowData>>(response);

            var result = new MazePageData { Id = pageNumber, Shows = showsData };

            return result;
        }
    }
}
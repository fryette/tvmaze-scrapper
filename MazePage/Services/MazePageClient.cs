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
    public class MazePageClient : IMazePageClient
    {
        private const string SHOWS_API_ENDPOINT = "http://api.tvmaze.com/shows";
        private const string GET_MAZE_PAGE_PATH_TEMPLATE = "?page={0}";
        private readonly HttpClient _client = new HttpClient { BaseAddress = new Uri(SHOWS_API_ENDPOINT) };

        private readonly Policy _exponentialRetryPolicy =
            Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                    //TODO: Should be used microservice for logging instedof Console
                    (ex, _) => Console.WriteLine(ex.ToString()));

        public async Task<IEnumerable<Show>> FetchShowsAsync(int pageNumber)
        {
            var response = string.Empty;

            await _exponentialRetryPolicy.ExecuteAsync(
                async () =>
                {
                    response = await _client.GetStringAsync(string.Format(GET_MAZE_PAGE_PATH_TEMPLATE, pageNumber)).ConfigureAwait(false);
                });

            var showsData = JsonConvert.DeserializeObject<List<Show>>(response);

            return showsData;
        }
    }
}
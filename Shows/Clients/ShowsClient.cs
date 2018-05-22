using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Shows.DataModels;

namespace Shows.Clients
{
    public class ShowsClient : IShowsClient
    {
        private const string SHOWS_API_ENDPOINT = "http://localhost:65373/shows";
        private const string GET_MAZE_PAGE_PATH_TEMPLATE = "?page={0}&from={1}&to={2}";

        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(SHOWS_API_ENDPOINT),
            DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
        };

        private readonly Policy _exponentialRetryPolicy =
            Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    3,
                    attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                    //TODO: Should be used microservice for logging instedof Console
                    (ex, _) => Console.WriteLine(ex.ToString()));

        public async Task<IReadOnlyList<Show>> FetchShowsAsync(int pageNumber, int from, int to)
        {
            var response = string.Empty;

            await _exponentialRetryPolicy.ExecuteAsync(
                async () =>
                {
                    response = await _client.GetStringAsync(string.Format(GET_MAZE_PAGE_PATH_TEMPLATE, pageNumber, from, to)).ConfigureAwait(false);
                });

            var shows = JsonConvert.DeserializeObject<List<Show>>(response);

            return shows;
        }
    }
}

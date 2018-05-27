using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Polly;
using Shows.Infrastructure;

namespace Shows.Clients
{
    public class CastClient : ICastClient
    {
        private const string CAST_API_ENDPOINT = "http://localhost:52348/casts";
        private const string REQUEST_PARAMETERS = "?ids=[{0}]";
        private readonly Policy _exponentialRetryPolicy =
            Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    2,
                    attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)),
                    //TODO: Should be used microservice for logging instedof Console
                    (ex, _) => Console.WriteLine(ex.ToString()));

        private readonly IsoDateTimeConverter _dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
        private readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(CAST_API_ENDPOINT),
            DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
        };

        public async Task<IEnumerable<ShowData>> FetchCastByShowIdAsync(IEnumerable<int> showIds)
        {
            var showData = Enumerable.Empty<ShowData>();

            await _exponentialRetryPolicy.ExecuteAsync(
                async () =>
                {
                    var response = await _client.GetStringAsync(string.Format(REQUEST_PARAMETERS, string.Join(',', showIds)));
                    showData = JsonConvert.DeserializeObject<List<ShowData>>(response, _dateTimeConverter);
                });

            return showData;
        }
    }
}

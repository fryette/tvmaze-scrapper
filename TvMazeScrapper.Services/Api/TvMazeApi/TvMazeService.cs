using System.Collections.Generic;
using System.Threading.Tasks;
using TvMazeScrapper.Infrastructure.Http;
using TvMazeScrapper.Infrastructure.Serializers;
using TvMazeScrapper.Models;

namespace TvMazeScrapper.Services.Api.TvMazeApi
{
    public class TvMazeService : ITvMazeApiService
    {
        private const string SHOWS_API_ENDPOINT = "http://api.tvmaze.com/shows?page={0}";
        private const string CAST_API_ENDPOINT = "http://api.tvmaze.com/shows/{0}/cast";
        private const string DATETIME_FORMAT = "yyyy-MM-dd";

        private readonly IHttpClient _client;
        private readonly IJsonConverter _json;

        public TvMazeService(IHttpClient client, IJsonConverter json)
        {
            _client = client;
            _json = json;
        }

        public async Task<IEnumerable<ShowModel>> FetchShowsAsync(int page)
        {
            return _json.Deserialize<List<ShowModel>>(await _client.GetAsync(string.Format(SHOWS_API_ENDPOINT, page)));
        }

        public async Task<List<PersonInfo>> FetchCastByShowIdAsync(string showId)
        {
            return _json.Deserialize<List<PersonInfo>>(await _client.GetAsync(string.Format(CAST_API_ENDPOINT, showId)), DATETIME_FORMAT);
        }
    }
}

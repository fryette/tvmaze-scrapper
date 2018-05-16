using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.Domain.TvMaze;
using TvMazeScrapper.Infrastructure.Http;
using TvMazeScrapper.Infrastructure.Interfaces.App;
using TvMazeScrapper.Infrastructure.Interfaces.DataServices;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi.DataModels;
using ShowData = TvMazeScrapper.Services.Api.TvMazeApi.DataModels.ShowData;

namespace TvMazeScrapper.Services.Api.TvMazeApi
{
    public class TvMazeService : ITvMazeApiService
    {
        private const string SHOWS_API_ENDPOINT = "http://api.tvmaze.com/shows?page={0}";
        private const string CAST_API_ENDPOINT = "http://api.tvmaze.com/shows/{0}/cast";
        private const string DATETIME_FORMAT = "yyyy-MM-dd";

        private readonly IHttpClient _client;
        private readonly IJsonConverter _json;
        private readonly IMapper _mapper;
        private readonly IPageRepository _pageRepository;

        public TvMazeService(IHttpClient client, IJsonConverter json, IMapper mapper, IPageRepository pageRepository)
        {
            _client = client;
            _json = json;
            _mapper = mapper;
            _pageRepository = pageRepository;
        }

        public async Task<IEnumerable<ShowModel>> FetchShowsAsync(int page)
        {
            var response = await _client.GetAsync(string.Format(SHOWS_API_ENDPOINT, page));
            var showsData = _json.Deserialize<List<ShowData>>(response);
            var result = _mapper.MapCollection<ShowData, ShowModel>(showsData).ToList();

            await _pageRepository.SaveTvMazePageAsync(
                new PageModel { Id = page, Shows = result });

            return result;
        }

        public async Task<List<PersonModel>> FetchCastByShowIdAsync(string showId)
        {
            var response = await _client.GetAsync(string.Format(CAST_API_ENDPOINT, showId));
            var personsInfoData = _json.Deserialize<List<PersonInfoData>>(response, DATETIME_FORMAT);

            return personsInfoData.Select(x => _mapper.Map<PersonModel>(x.Person)).ToList();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TvMazeScrapper.DataAccess;
using TvMazeScrapper.Domain.TvMaze;
using TvMazeScrapper.Infrastructure.Interfaces;
using TvMazeScrapper.Infrastructure.Interfaces.App;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api.TvMazeApi.DataModels;

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
        private readonly IRepository<TvMazePage> _repository;

        public TvMazeService(
            IHttpClient client,
            IJsonConverter json,
            IMapper mapper,
            IRepository<TvMazePage> repository)
        {
            _client = client;
            _json = json;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PageModel> FetchShowsAsync(int pageNumber)
        {
            var response = await _client.GetAsync(string.Format(SHOWS_API_ENDPOINT, pageNumber));
            var showsData = _json.Deserialize<List<ShowData>>(response);
            var showsModel = _mapper.MapCollection<ShowData, ShowModel>(showsData).ToList();

            var result = new PageModel {Id = pageNumber, Shows = showsModel};

            await _repository.SaveAsync(_mapper.Map<TvMazePage>(result));

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
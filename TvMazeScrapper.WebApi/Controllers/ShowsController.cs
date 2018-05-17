using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Models.App;

namespace TvMazeScrapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ShowsController : Controller
    {
        private readonly IScrapperApiService _apiService;

        public ShowsController(IScrapperApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShowModel>> Get(int page = 0)
        {
            return await _apiService.LoadShowsAsync(page).ConfigureAwait(false);
        }
    }
}
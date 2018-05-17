using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScrapper.Infrastructure.Interfaces.Api;

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
        public async Task<IActionResult> Get(int page = 0)
        {
            if (page < 0)
            {
                return NotFound();
            }

            var data = await _apiService.LoadShowsAsync(page).ConfigureAwait(false);

            if (data.Any())
            {
                return Ok(data);
            }

            return NoContent();
        }
    }
}
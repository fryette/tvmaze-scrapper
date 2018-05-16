using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TvMazeScrapper.Infrastructure.Interfaces.Api;
using TvMazeScrapper.Models;
using TvMazeScrapper.Models.App;
using TvMazeScrapper.Services.Api;

namespace TvMazeScrapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IScrapperApiService _apiService;

        public ValuesController(IScrapperApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IEnumerable<ShowModel>> Get()
        {
            return await _apiService.LoadShowsAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

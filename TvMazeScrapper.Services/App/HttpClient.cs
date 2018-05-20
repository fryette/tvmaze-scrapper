using System.Threading.Tasks;
using TvMazeScrapper.Infrastructure.Interfaces;

namespace TvMazeScrapper.Services.App
{
    public class HttpClient : IHttpClient
    {
        private static readonly System.Net.Http.HttpClient Client = new System.Net.Http.HttpClient();

        public async Task<string> GetAsync(string url)
        {
            return await Client.GetStringAsync(url);
        }
    }
}
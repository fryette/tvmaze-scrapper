using System.Threading.Tasks;

namespace TvMazeScrapper.Infrastructure.Http
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string url);
    }
}

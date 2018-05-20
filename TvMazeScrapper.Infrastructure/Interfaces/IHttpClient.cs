using System.Threading.Tasks;

namespace TvMazeScrapper.Infrastructure.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetAsync(string url);
    }
}

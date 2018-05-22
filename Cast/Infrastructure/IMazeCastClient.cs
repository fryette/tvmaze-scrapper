using System.Threading.Tasks;

namespace Cast.Infrastructure
{
    public interface IMazeCastClient
    {
        Task<DataModels.Cast> FetchCastByShowIdAsync(int showId);
    }
}

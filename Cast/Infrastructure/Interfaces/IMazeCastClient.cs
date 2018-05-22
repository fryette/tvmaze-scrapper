using System.Threading.Tasks;

namespace Cast.Infrastructure.Interfaces
{
    public interface IMazeCastClient
    {
        Task<DataModels.Cast> FetchCastByShowIdAsync(int showId);
    }
}

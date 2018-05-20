using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cast
{
    public interface IMazeCastClient
    {
        Task<DataModels.Cast> FetchCastByShowIdAsync(int showId);
    }
}

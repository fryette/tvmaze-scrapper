using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cast.Infrastructure
{
    public interface IMazeCastStore
    {
        Task SaveCastsAsync(List<DataModels.Cast> casts);
        Task<List<DataModels.Cast>> GetCastsAsync(IReadOnlyList<int> showIdList);
    }
}
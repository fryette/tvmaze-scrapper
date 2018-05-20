using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cast.DataAccess
{
    public interface IMazeCastStore
    {
        Task SaveCastsAsync(List<DataModels.Cast> casts);
    }
}
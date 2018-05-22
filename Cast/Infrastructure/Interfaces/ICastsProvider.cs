using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cast.Infrastructure
{
    public interface ICastsProvider
    {
        Task<IEnumerable<DataModels.Cast>> LoadCastAsync(int[] showIds);
    }
}

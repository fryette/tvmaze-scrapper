using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shows.Clients
{
    public interface ICastClient
    {
        Task<IEnumerable<ShowData>> FetchCastByShowIdAsync(IEnumerable<int> showIds);
    }
}
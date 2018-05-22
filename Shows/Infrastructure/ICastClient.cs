using System.Collections.Generic;
using System.Threading.Tasks;
using Shows.Clients;

namespace Shows.Infrastructure
{
    public interface ICastClient
    {
        Task<IEnumerable<ShowData>> FetchCastByShowIdAsync(IEnumerable<int> showIds);
    }
}
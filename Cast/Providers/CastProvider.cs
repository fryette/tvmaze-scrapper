using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cast.Infrastructure;
using Cast.Infrastructure.Interfaces;

namespace Cast.Providers
{
    public class CastProvider : ICastsProvider
    {
        private readonly IMazeCastClient _client;
        private readonly IMazeCastStore _store;

        public CastProvider(IMazeCastClient client, IMazeCastStore store)
        {
            _client = client;
            _store = store;
        }

        public async Task<IEnumerable<DataModels.Cast>> LoadCastAsync(int[] showIds)
        {
            var result = await _store.GetCastsAsync(showIds);

            var uncachedItemIds = showIds.Except(result.Select(x => x.ShowId)).ToList();

            var fetchedResult = new List<DataModels.Cast>(uncachedItemIds.Count);

            foreach (var id in uncachedItemIds)
            {
                var cast = await _client.FetchCastByShowIdAsync(id);

                if (cast != null)
                {
                    fetchedResult.Add(cast);
                }
            }

            if (fetchedResult.Any())
            {
                await _store.SaveCastsAsync(fetchedResult);
                result.AddRange(fetchedResult);
            }

            return result;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Cast.DataAccess;
using Nancy;
using Newtonsoft.Json;

namespace Cast
{
    public sealed class CastModule : NancyModule
    {
        public CastModule(IMazeCastClient client, IMazeCastStore store) : base("/casts")
        {
            Get(
                "/",
                async _ =>
                {
                    var ids = Request.Query["ids"];

                    if (ids == null)
                    {
                        return new Response
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ReasonPhrase = "ids http parameter not found"
                        };
                    }

                    int[] castIds = JsonConvert.DeserializeObject<int[]>(ids);

                    if (!castIds.Any())
                    {
                        return new Response
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ReasonPhrase = "ids property should contain at least one id"
                        };
                    }

                    var result = new List<DataModels.Cast>();

                    foreach (var id in castIds)
                    {
                        var cast = await client.FetchCastByShowIdAsync(id);
                        if (cast != null)
                        {
                            result.Add(cast);
                        }
                    }

                    await store.SaveCastsAsync(result);

                    return null;
                });
        }
    }
}

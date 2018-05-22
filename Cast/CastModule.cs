using System.Linq;
using Cast.Infrastructure.Interfaces;
using Nancy;
using Newtonsoft.Json;

namespace Cast
{
    public sealed class CastModule : NancyModule
    {
        public CastModule(ICastsProvider provider) : base("/casts")
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

                    int[] showIds = JsonConvert.DeserializeObject<int[]>(ids);

                    if (!showIds.Any())
                    {
                        return new Response
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ReasonPhrase = "ids property should contain at least one id"
                        };
                    }

                    return Negotiate.WithModel(await provider.LoadCastAsync(showIds)).WithHeader("cache-control", "max-age:86400");
                });
        }
    }
}

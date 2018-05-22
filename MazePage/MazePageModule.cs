using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MazePage.DataModels;
using MazePage.Infrastructure;
using Nancy;

namespace MazePage
{
    public sealed class MazePageModule : NancyModule
    {
        public MazePageModule(IMazePageClient mazePageClient, IMazePageStore store) : base("/shows")
        {
            Get(
                "/",
                async _ =>
                {
                    IEnumerable<Show> result = await store.LoadShowsByPageIdAsync(Request.Query.page);

                    if (!result.Any())
                    {
                        result = await mazePageClient.FetchShowsAsync(Request.Query.page).ConfigureAwait(false);
                        await store.SaveMazePageAsync(Request.Query.page, result).ConfigureAwait(false);
                    }

                    var to = 0;
                    if (int.TryParse(Request.Query["from"], out int from) && int.TryParse(Request.Query["to"], out to))
                    {
                        result = result.Skip(from).Take(to - from);
                    }

                    return Negotiate.WithModel(result).WithHeader("cache-control", "max-age:86400");
                });
        }
    }
}

﻿using System.Collections.Generic;
using Nancy;
using Shows.DataModels;

namespace Shows
{
    public sealed class ShowsModule : NancyModule
    {
        public ShowsModule(IScrapperShowsService showsService) : base("/shows")
        {
            Get(
                "/",
                async _ =>
                {
                    IEnumerable<Show> result = await showsService.LoadShowsAsync(Request.Query.page);
                    return Negotiate.WithModel(result).WithHeader("cache-control", "max-age:86400");
                });
        }
    }
}

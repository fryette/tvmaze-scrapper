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
                    MazePageData result = await store.GetMazePageAsync(Request.Query.page);

                    if (result == null)
                    {
                        result = await mazePageClient.FetchShowsAsync(Request.Query.page);
                        await store.SaveMazePageAsync(result);
                    }

                    return Negotiate.WithModel(result).WithHeader("cache-control", "max-age:86400");
                });
        }
    }
}

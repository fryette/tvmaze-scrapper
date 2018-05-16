using System.Collections.Generic;

namespace TvMazeScrapper.Models.App
{
    public class PageModel
    {
        public int Id { get; set; }
        public IEnumerable<ShowModel> Shows { get; set; }
    }
}

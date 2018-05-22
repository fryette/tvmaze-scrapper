using System.Collections.Generic;

namespace MazePage.DataModels
{
    public class MazePageData
    {
        public int Id { get; set; }
        public IEnumerable<Show> Shows { get; set; }
    }
}

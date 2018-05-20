using System.Collections.Generic;

namespace MazePage.DataModels
{
    public class MazePageData
    {
        public int Id { get; set; }
        public IEnumerable<ShowData> Shows { get; set; }
    }
}

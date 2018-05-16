using System.ComponentModel.DataAnnotations;

namespace TvMazeScrapper.Domain.TvMaze
{
    public class TvMazeShow
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

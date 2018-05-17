using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Domain.TvMaze
{
    public class TvMazePage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public IEnumerable<TvMazeShow> Shows { get; set; }
    }
}
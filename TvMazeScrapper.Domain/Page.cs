using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Domain
{
    public class Page
    {
        public Page()
        {

        }

        public Page(int pageNumber)
        {
            Id = pageNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public List<Show> Shows { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Models
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
        public List<ShowModel> Shows { get; set; }
    }
}

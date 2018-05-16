using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TvMazeScrapper.Models.App
{
    public class PageModel
    {
        public PageModel()
        {

        }

        public PageModel(int pageNumber)
        {
            Id = pageNumber;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public List<ShowModel> Shows { get; set; }
    }
}

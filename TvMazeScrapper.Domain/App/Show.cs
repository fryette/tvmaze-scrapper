using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TvMazeScrapper.Domain.App;

namespace TvMazeScrapper.Domain
{
    public class Show
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRow { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Person> Cast { get; set; }
    }
}

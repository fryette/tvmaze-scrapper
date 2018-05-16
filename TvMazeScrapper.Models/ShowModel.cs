using System.Collections.Generic;

namespace TvMazeScrapper.Models
{
    public class ShowModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Person> Cast { get; set; }
    }
}

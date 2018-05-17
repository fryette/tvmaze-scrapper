using System.Collections.Generic;

namespace TvMazeScrapper.Models.App
{
    public class ShowModel
    {
        public List<PersonModel> Cast { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
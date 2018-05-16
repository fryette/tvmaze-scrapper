using System;
using System.Collections.Generic;
using System.Text;

namespace TvMazeScrapper.Domain
{
    public class Show
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Person> Cast { get; set; }
    }
}

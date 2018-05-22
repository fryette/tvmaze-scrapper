using System.Collections.Generic;
using Shows.DataModels;

namespace Shows.Clients
{
    public class ShowData
    {
        public int ShowId { get; set; }
        public List<Person> Persons { get; set; }
    }
}

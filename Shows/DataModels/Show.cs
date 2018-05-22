using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shows.DataModels
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Cast { get; set; }
    }
}

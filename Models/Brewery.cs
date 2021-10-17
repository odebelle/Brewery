using System.Collections.Generic;

namespace Models
{
    public class Brewery
    {
        public int BreweryId { get; set; }
        public string Name { get; set; }

        public ICollection<Bier> Biers { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
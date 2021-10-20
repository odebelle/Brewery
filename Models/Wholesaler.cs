using System.Collections.Generic;

namespace Models
{
    public class Wholesaler
    {
        public int WholesalerId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public ICollection<Bier> Biers { get; set; }
    }
}
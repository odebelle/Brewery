using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int WholesalerId { get; set; }

        public ICollection<LineOrder> LineOrders { get; set; }

        public double TotalPrice { get; set; }
    }
}
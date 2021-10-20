using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Order
    {
        private double _price;
        public int OrderId { get; set; }
        public int WholesalerId { get; set; }

        public ICollection<LineOrder> LineOrders { get; set; }

        public double Price
        {
            get => _price;
        }

        public double Discount
        {
            get
            {
                int count = LineOrders.Count;
                switch (count)
                {
                    case >20:
                        return 0.8;
                    case >10:
                        return 0.9;
                }
                return 1;
            }
        }

        public double TotalPrice
        {
            get
            {
                _price = LineOrders.Sum(s => s.Bier.Price);
                return Price * Discount;
            }
        }
    }
}
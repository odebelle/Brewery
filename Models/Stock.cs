namespace Models
{
    public class Stock
    {
        public int StockId { get; set; }
        public int BierId { get; set; }
        public int WholesalerID { get; set; }

        public Wholesaler Wholesaler { get; set; }
        public Bier Type { get; set; }

        public int Count { get; set; }
    }
}
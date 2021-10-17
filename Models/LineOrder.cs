namespace Models
{
    public class LineOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BierId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Bier Bier { get; set; }
    }
}
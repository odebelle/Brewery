using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bier> Biers { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineOrder> LineOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bier>().ToTable(nameof(Bier));
            modelBuilder.Entity<Brewery>().ToTable(nameof(Brewery));
            modelBuilder.Entity<Wholesaler>().ToTable(nameof(Wholesaler));
            modelBuilder.Entity<Stock>().ToTable(nameof(Stock));
            modelBuilder.Entity<Order>().ToTable(nameof(Order));
            modelBuilder.Entity<LineOrder>().ToTable(nameof(LineOrder));
        }
    }
}
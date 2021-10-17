using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Bier
    {
        public int BierId { get; set; }
        public string Name { get; set; }
        public double Degree { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }

        public int BreweryId { get; set; }
        public Brewery Brewery { get; set; }
    }

    public class Stock
    {
        public int StockId { get; set; }
        public int BierId { get; set; }
        public int WholesalerID { get; set; }

        public Wholesaler Wholesaler { get; set; }
        public Bier Type { get; set; }

        public int Count { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int WholesalerId { get; set; }

        public ICollection<LineOrder> LineOrders { get; set; }

        public double TotalPrice { get; set; }
    }

    public class LineOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BierId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Bier Bier { get; set; }
    }

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

    public static class DbInitializer
    {
        public static void Initialize(BreweryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Biers.Any())
            {
                return; // DB has been seeded
            }

            for (int b = 0; b < 4; b++)
            {
                var brewery = new Brewery
                {
                    Name = $"Brewery {b}" //, BreweryId = b
                };

                // context.Breweries.Add(brewery);
                // context.SaveChanges();

                for (int i = 0; i < 10; i++)
                {
                    context.Biers.Add(new Bier
                    {
                        //BierId = i,
                        Available = true,
                        //BreweryId = b,
                        Brewery = brewery,
                        Degree = i * 2,
                        Name = $"Bier {b}_{i}",
                        Price = i * 2.5
                    });
                    context.SaveChanges();
                }
            }

            for (int w = 0; w < 6; w++)
            {
                var wholesaler = new Wholesaler
                {
                    Name = $"Wholesaler {w}"
                };
                context.Wholesalers.Add(wholesaler);
            }

            context.SaveChanges();
        }
    }
}
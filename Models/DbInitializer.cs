using System.Linq;

namespace Models
{
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
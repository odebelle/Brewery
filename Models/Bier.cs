using System;
using System.Reflection.Metadata;

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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using LuxuryCarRental.Models;

namespace LuxuryCarRental.Data
{
    public class AppDbContext : DbContext
    {
        // Parameterless ctor for EF tooling
        public AppDbContext() { }

        // ctor for DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets for all your entities
        public DbSet<Car> Cars { get; set; }
        public DbSet<LuxuryCar> LuxuryCars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // Uses a file named LuxuryRental.db in your app's working directory
                options.UseSqlite("Data Source=LuxuryRental.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If you want EF to treat ContactInfo as an owned (complex) type:
            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.Contact);

            // You can also configure indices, relationships, JSON fields, etc.
            // e.g. index on Car.Make + Car.Model:
            modelBuilder.Entity<Car>()
                .HasIndex(c => new { c.Make, c.Model });
        }
    }
}
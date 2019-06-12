using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop5_CPRG214_WebApp.Models
{
    public class TravelExpertDBContext : DbContext
    {
        public TravelExpertDBContext(DbContextOptions<TravelExpertDBContext> options) : base(options)
        {

        }

        public DbSet<Packages> Packages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<TripTypes> TripTypes { get; set; }
        public DbSet<Packages_Products_Suppliers> Packages_Products_Suppliers { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Packages>().ToTable("Packages");
        //    modelBuilder.Entity<Customer>().ToTable("Customer");
        //}
    }

}

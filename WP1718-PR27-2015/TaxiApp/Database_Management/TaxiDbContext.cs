using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TaxiApp.Models;

namespace TaxiApp.Database_Management
{
    public class TaxiDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TaxiDrive> TaxiDrives { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Location> Locations { get; set; }

        public TaxiDbContext() : base("name=TaxiDbConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaxiDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //many to......
            //modelBuilder.Entity<>
        }
    }
}
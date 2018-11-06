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
            modelBuilder.Entity<Admin>().HasKey(a => a.Username);
            modelBuilder.Entity<Admin>().HasMany(a => a.TaxiDrives)
                                        .WithOptional(td => td.TaxiDriveDispatcher);

            modelBuilder.Entity<Customer>().HasKey(c => c.Username);
            modelBuilder.Entity<Customer>().HasMany(c => c.TaxiDrives)
                                           .WithOptional(td => td.TaxiDriveCustomer);

            modelBuilder.Entity<Driver>().HasKey(d => d.Username);
            modelBuilder.Entity<Driver>().HasMany(d => d.TaxiDrives)
                                         .WithOptional(td => td.TaxiDriveDriver);
            modelBuilder.Entity<Driver>().HasOptional(d => d.DriversLocation);

            modelBuilder.Entity<Vehicle>().HasKey(v => v.VehicleID);
            modelBuilder.Entity<Vehicle>().HasOptional(v => v.VehicleDriver)
                                          .WithOptionalPrincipal(d => d.DriversVehicle);

            modelBuilder.Entity<TaxiDrive>().HasKey(td => td.TaxiDriveID);
            modelBuilder.Entity<TaxiDrive>().HasOptional(td => td.TaxiDriveStartingLocation);
            modelBuilder.Entity<TaxiDrive>().HasOptional(td => td.TaxiDriveDestination);
            modelBuilder.Entity<TaxiDrive>().HasOptional(td => td.TaxiDriveComment)
                                            .WithOptionalPrincipal(c => c.CommentedTaxiDrive);

            modelBuilder.Entity<Comment>().HasKey(c => c.CommentID);
            modelBuilder.Entity<Comment>().HasOptional(c => c.CommentOwner);

            modelBuilder.Entity<Location>().HasKey(l => l.LocationID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
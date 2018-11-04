using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using TaxiApp.Models;

namespace TaxiApp.Database_Management
{
    public class Configuration : DbMigrationsConfiguration<TaxiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "Context";
        }

        protected override void Seed(TaxiDbContext context)
        {
            Admin admin = new Admin()
            {

            };
            //add admin admin
        }
    }
}
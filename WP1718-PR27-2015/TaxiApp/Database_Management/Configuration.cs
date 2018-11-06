using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TaxiApp.Database_Management.Access;
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
            Admin admin = new Admin("admin", "admin")
            {
                FirstName = "Pera",
                LastName = "Peric",
                Gender = Common.EGender.MALE,
                Email = "admin@admin",
                Phone = "0214",
                JMBG = "000",
            };

            AdminDbAccess db = DbAccess.Instance.AdminDbAccess;

            try
            {
                if(db.Add(admin))
                {
                    Trace.Write("Admin added.");
                }
                else
                {
                    Trace.Write("Admin already exists.");
                }
            }
            catch(Exception e)
            {
                Trace.Write($"Erorr on adding admin. Error message: {e.Message}");
                Trace.Write($"[STACK_TRACE] {e.StackTrace}");
            }
        }
    }
}
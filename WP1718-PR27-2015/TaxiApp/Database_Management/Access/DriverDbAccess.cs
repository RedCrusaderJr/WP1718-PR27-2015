using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class DriverDbAccess : BaseDbAccess<Driver, string>
    {
        #region Instance
        private static DriverDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static DriverDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DriverDbAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public override bool Add(Driver entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Drivers.Any(d => d.Username.Equals(entityToAdd.Username)))
                {
                    try
                    {
                        db.Drivers.Add(entityToAdd);
                        db.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override bool Modify(Driver entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Drivers.Any(d => d.Username.Equals(entityToModify.Username)))
                {
                    try
                    {
                        Driver foundDriver = db.Drivers.Include(d => d.TaxiDrives)
                                                       .Include(d => d.DriversLocation)
                                                       .Include(d => d.DriversVehicle)
                                                       .SingleOrDefault(d => d.Username.Equals(entityToModify.Username));
                        db.Drivers.Attach(foundDriver);

                        foundDriver.FirstName = entityToModify.FirstName;
                        foundDriver.LastName = entityToModify.LastName;
                        foundDriver.Password = entityToModify.Password;
                        foundDriver.Gender = entityToModify.Gender;
                        foundDriver.JMBG = entityToModify.JMBG;
                        foundDriver.Phone = entityToModify.Phone;
                        foundDriver.Email = entityToModify.Email;

                        //dodaj nove: sa new?
                        entityToModify.TaxiDrives.Where(td => !foundDriver.TaxiDrives.Contains(td)).ToList().ForEach(td => foundDriver.TaxiDrives.Add(td));
                        //izbaci one kojih vise nema
                        foundDriver.TaxiDrives.Where(td => !entityToModify.TaxiDrives.Contains(td)).ToList().ForEach(td => foundDriver.TaxiDrives.Remove(td));

                        foundDriver.DriversLocation = entityToModify.DriversLocation; //new?
                        foundDriver.DriversVehicle = entityToModify.DriversVehicle; //new?

                        db.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override bool Delete(Driver entityToDelete)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Drivers.Any(d => d.Username.Equals(entityToDelete.Username)))
                {
                    try
                    {
                        db.Drivers.Remove(entityToDelete);
                        //LOCATION?

                        db.SaveChanges();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override Driver GetSingleEntityByKey(string key)
        {
            Driver result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Drivers.Any(c => c.Username.Equals(key)))
                {
                    try
                    {
                        result = db.Drivers.Include(d => d.TaxiDrives)
                                           .Include(d => d.DriversLocation)
                                           .Include(d => d.DriversVehicle)
                                           .FirstOrDefault(d => d.Username.Equals(key));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Driver> GetAll()
        {
            List<Driver> result = new List<Driver>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if (db.Drivers.Count() > 0)
                    {
                        result = new List<Driver>(db.Drivers.Include(d => d.TaxiDrives)
                                                            .Include(d => d.DriversLocation)
                                                            .Include(d => d.DriversVehicle)
                                                            .ToList());
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return result;
        }
    }
}
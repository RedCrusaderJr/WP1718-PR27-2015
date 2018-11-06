using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class VehicleDbAccess : BaseDbAccess<Vehicle, string>
    {
        #region Instance
        private static VehicleDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static VehicleDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new VehicleDbAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public override bool Add(Vehicle entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Vehicles.Any(v => v.VehicleID.Equals(entityToAdd.VehicleID)))
                {
                    try
                    {
                        db.Vehicles.Add(entityToAdd);
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

        public override bool Modify(Vehicle entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Vehicles.Any(v => v.VehicleID.Equals(entityToModify.VehicleID)))
                {
                    try
                    {
                        Vehicle foundVehicle = db.Vehicles.Include(v => v.VehicleDriver)
                                                          .SingleOrDefault(v => v.VehicleID.Equals(entityToModify.VehicleID));
                        db.Vehicles.Attach(foundVehicle);

                        foundVehicle.LicencePlateNo = entityToModify.LicencePlateNo;
                        foundVehicle.ProductionYear = entityToModify.ProductionYear;
                        foundVehicle.VehicleType = entityToModify.VehicleType;

                        foundVehicle.VehicleDriver = entityToModify.VehicleDriver; //NEW
                        
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

        public override bool Delete(Vehicle entityToDelete)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Vehicles.Any(v => v.VehicleID.Equals(entityToDelete.VehicleID)))
                {
                    try
                    {
                        db.Vehicles.Remove(entityToDelete);

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

        public override Vehicle GetSingleEntityByKey(string key)
        {
            Vehicle result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Vehicles.Any(v => v.VehicleID.Equals(key)))
                {
                    try
                    {
                        result = db.Vehicles.Include(v => v.VehicleDriver)
                                            .FirstOrDefault(v => v.VehicleID.Equals(key));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Vehicle> GetAll()
        {
            List<Vehicle> result = new List<Vehicle>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if (db.Vehicles.Count() > 0)
                    {
                        result = new List<Vehicle>(db.Vehicles.Include(v => v.VehicleDriver).ToList());
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class LocationDbAccess : BaseDbAccess<Location, string>
    {
        #region Instance
        private static LocationDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static LocationDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LocationDbAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public override bool Add(Location entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Locations.Any(l => l.LocationID.Equals(entityToAdd.LocationID)))
                {
                    try
                    {
                        db.Locations.Add(entityToAdd);
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

        public override bool Modify(Location entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Locations.Any(l => l.LocationID.Equals(entityToModify.LocationID)))
                {
                    try
                    {
                        Location foundLocation = db.Locations.SingleOrDefault(l => l.LocationID.Equals(entityToModify.LocationID));
                        db.Locations.Attach(foundLocation);

                        //foundLocation.XCoordinate = entityToModify.XCoordinate;
                        //foundLocation.YCoordinate = entityToModify.YCoordinate;
                        foundLocation.StreetName = entityToModify.StreetName;
                        foundLocation.StreetNumber = entityToModify.StreetNumber;
                        foundLocation.City = entityToModify.City;
                        foundLocation.ZipCode = entityToModify.ZipCode;
                        
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

        public override bool Delete(Location entityToDelete)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Locations.Any(l=> l.LocationID.Equals(entityToDelete.LocationID)))
                {
                    try
                    {
                        db.Locations.Remove(entityToDelete);

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

        public override Location GetSingleEntityByKey(string key)
        {
            Location result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Locations.Any(l => l.LocationID.Equals(key)))
                {
                    try
                    {
                        result = db.Locations.FirstOrDefault(l => l.LocationID.Equals(key));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Location> GetAll()
        {
            List<Location> result = new List<Location>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if (db.Locations.Count() > 0)
                    {
                        result = new List<Location>(db.Locations.ToList());
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
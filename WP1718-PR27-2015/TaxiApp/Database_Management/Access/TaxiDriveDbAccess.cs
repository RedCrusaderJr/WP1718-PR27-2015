using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class TaxiDriveDbAccess : IDbAccess<TaxiDrive, string>
    {
        public bool Add(TaxiDrive entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.TaxiDrives.Any(td => td.TaxiDriveID.Equals(entityToAdd.TaxiDriveID)))
                {
                    try
                    {
                        db.TaxiDrives.Add(entityToAdd);
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

        public bool Delete(TaxiDrive entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaxiDrive> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaxiDrive GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(TaxiDrive entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class LocationDbAccess : IDbAccess<Location, string>
    {
        public bool Add(Location entityToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Location entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAll()
        {
            throw new NotImplementedException();
        }

        public Location GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(Location entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
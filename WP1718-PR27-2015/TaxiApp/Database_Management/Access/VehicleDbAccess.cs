using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class VehicleDbAccess : IDbAccess<Vehicle, string>
    {
        public bool Add(Vehicle entityToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Vehicle entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vehicle> GetAll()
        {
            throw new NotImplementedException();
        }

        public Vehicle GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(Vehicle entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
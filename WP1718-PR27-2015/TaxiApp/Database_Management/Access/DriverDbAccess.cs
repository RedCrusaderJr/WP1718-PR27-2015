using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class DriverDbAccess : IDbAccess<Driver, string>
    {
        public bool Add(Driver entityToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Driver entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Driver> GetAll()
        {
            throw new NotImplementedException();
        }

        public Driver GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(Driver entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
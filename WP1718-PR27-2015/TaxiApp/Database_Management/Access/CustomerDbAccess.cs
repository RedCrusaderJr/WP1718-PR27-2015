using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApp.Common;
using TaxiApp.Models;

namespace TaxiApp.Database_Management.Access
{
    public class CustomerDbAccess : IDbAccess<Customer, string>
    {
        public bool Add(Customer entityToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Customer entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(Customer entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}

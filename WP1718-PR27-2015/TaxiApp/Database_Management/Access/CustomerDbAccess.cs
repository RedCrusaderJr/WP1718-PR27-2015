using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class CustomerDbAccess : BaseDbAccess<Customer, string>
    {
        #region Instance
        private static CustomerDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static CustomerDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomerDbAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public override bool Add(Customer entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Customers.Any(c => c.Username.Equals(entityToAdd.Username)))
                {
                    try
                    {
                        db.Customers.Add(entityToAdd);
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

        public override bool Modify(Customer entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Customers.Any(c => c.Username.Equals(entityToModify.Username)))
                {
                    try
                    {
                        Customer foundCustomer = db.Customers.Include(c => c.TaxiDrives)
                                                             .SingleOrDefault(c => c.Username.Equals(entityToModify.Username));
                        db.Customers.Attach(foundCustomer);

                        foundCustomer.FirstName = entityToModify.FirstName;
                        foundCustomer.LastName = entityToModify.LastName;
                        foundCustomer.Password = entityToModify.Password;
                        foundCustomer.Gender = entityToModify.Gender;
                        foundCustomer.JMBG = entityToModify.JMBG;
                        foundCustomer.Phone = entityToModify.Phone;
                        foundCustomer.Email = entityToModify.Email;

                        //dodaj nove
                        entityToModify.TaxiDrives.Where(td => !foundCustomer.TaxiDrives.Contains(td)).ToList().ForEach(td => foundCustomer.TaxiDrives.Add(td));

                        //izbaci one kojih vise nema
                        foundCustomer.TaxiDrives.Where(td => !entityToModify.TaxiDrives.Contains(td)).ToList().ForEach(td => foundCustomer.TaxiDrives.Remove(td));

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

        public override bool Delete(string entityToDeleteID)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Customers.Any(c => c.Username.Equals(entityToDeleteID)))
                {
                    try
                    {
                        Customer entityToDelete = db.Customers.FirstOrDefault(c => c.Username.Equals(entityToDeleteID));
                        db.Customers.Remove(entityToDelete);

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

        public override Customer GetSingleEntityByKey(string key)
        {
            Customer result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Customers.Any(c => c.Username.Equals(key)))
                {
                    try
                    {
                        result = db.Customers.Include(c => c.TaxiDrives).FirstOrDefault(c => c.Username.Equals(key));   
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Customer> GetAll()
        {
            List<Customer> result = new List<Customer>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if (db.Customers.Count() > 0)
                    {
                        result = new List<Customer>(db.Customers.Include(c => c.TaxiDrives).ToList());
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return result;
        }

        public override bool Exists(string key)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Customers.Any(c => c.Username.Equals(key)))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}

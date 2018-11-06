using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class AdminDbAccess : BaseDbAccess<Admin, string>
    {
        #region Instance
        private static AdminDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static AdminDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AdminDbAccess();
                        }
                    }
                }
                return _instance;
            }
        } 
        #endregion

        public override bool Add(Admin entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Admins.Any(a => a.Username.Equals(entityToAdd.Username)))
                {
                    try
                    {
                        db.Admins.Add(entityToAdd);
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

        public override bool Modify(Admin entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Admins.Any(a => a.Username.Equals(entityToModify.Username)))
                {
                    try
                    {
                        Admin foundAdmin = db.Admins.Include(a => a.TaxiDrives).SingleOrDefault(a => a.Username.Equals(entityToModify.Username));
                        db.Admins.Attach(foundAdmin);

                        foundAdmin.FirstName = entityToModify.FirstName;
                        foundAdmin.LastName = entityToModify.LastName;
                        foundAdmin.Password = entityToModify.Password;
                        foundAdmin.Gender = entityToModify.Gender;
                        foundAdmin.JMBG = entityToModify.JMBG;
                        foundAdmin.Phone = entityToModify.Phone;
                        foundAdmin.Email = entityToModify.Email;

                        //dodaj nove
                        entityToModify.TaxiDrives.Where(td => !foundAdmin.TaxiDrives.Contains(td)).ToList().ForEach(td => foundAdmin.TaxiDrives.Add(td));

                        //izbaci one kojih vise nema
                        foundAdmin.TaxiDrives.Where(td => !entityToModify.TaxiDrives.Contains(td)).ToList().ForEach(td => foundAdmin.TaxiDrives.Remove(td));

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

        public override bool Delete(Admin entityToDelete)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Admins.Any(a => a.Username.Equals(entityToDelete.Username)))
                {
                    try
                    {
                        db.Admins.Remove(entityToDelete);

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

        public override Admin GetSingleEntityByKey(string key)
        {
            Admin result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Admins.Any(a => a.Username.Equals(key)))
                {
                    try
                    {
                        result = db.Admins.Include(a => a.TaxiDrives).FirstOrDefault(a => a.Username.Equals(key));
                    }
                    catch(Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Admin> GetAll()
        {
            List<Admin> result = new List<Admin>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if(db.Admins.Count() > 0)
                    {
                        result = new List<Admin>(db.Admins.Include(a => a.TaxiDrives).ToList());
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
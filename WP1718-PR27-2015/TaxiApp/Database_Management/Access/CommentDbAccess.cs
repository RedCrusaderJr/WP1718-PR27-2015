using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class CommentDbAccess : BaseDbAccess<Comment, string>
    {
        #region Instance
        private static CommentDbAccess _instance;
        private static readonly object _syncLock = new object();
        public static CommentDbAccess Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CommentDbAccess();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public override bool Add(Comment entityToAdd)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (!db.Comments.Any(c => c.CommentID.Equals(entityToAdd.CommentID)))
                {
                    try
                    {
                        db.Comments.Add(entityToAdd);
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

        public override bool Modify(Comment entityToModify)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Comments.Any(c => c.CommentID.Equals(entityToModify.CommentID)))
                {
                    try
                    {
                        Comment foundComment = db.Comments.Include(c => c.CommentOwner)
                                                          .Include(c => c.CommentedTaxiDrive)
                                                          .SingleOrDefault(c => c.CommentID.Equals(entityToModify.CommentID));
                        db.Comments.Attach(foundComment);

                        foundComment.Description = entityToModify.Description;
                        foundComment.TaxiDriveRate = entityToModify.TaxiDriveRate;
                        foundComment.CommentOwner = entityToModify.CommentOwner; //NEW
                        foundComment.CommentedTaxiDrive = entityToModify.CommentedTaxiDrive; //NEW

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

        public override bool Delete(Comment entityToDelete)
        {
            bool result = false;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Comments.Any(c => c.CommentID.Equals(entityToDelete.CommentID)))
                {
                    try
                    {
                        db.Comments.Remove(entityToDelete);

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

        public override Comment GetSingleEntityByKey(string key)
        {
            Comment result = null;

            using (TaxiDbContext db = new TaxiDbContext())
            {
                if (db.Comments.Any(c => c.CommentID.Equals(key)))
                {
                    try
                    {
                        result = db.Comments.Include(c => c.CommentOwner)
                                            .Include(c => c.CommentedTaxiDrive)
                                            .FirstOrDefault(a => a.CommentID.Equals(key));
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return result;
        }

        public override IEnumerable<Comment> GetAll()
        {
            List<Comment> result = new List<Comment>();

            using (TaxiDbContext db = new TaxiDbContext())
            {
                try
                {
                    if (db.Comments.Count() > 0)
                    {
                        result = new List<Comment>(db.Comments.Include(c => c.CommentOwner)
                                                              .Include(c => c.CommentedTaxiDrive));
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
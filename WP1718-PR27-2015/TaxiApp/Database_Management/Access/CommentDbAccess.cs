using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;
using TaxiApp.Models;
using System.Data.Entity;

namespace TaxiApp.Database_Management.Access
{
    public class CommentDbAccess : IDbAccess<Comment, string>
    {
        public bool Add(Comment entityToAdd)
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

        public bool Modify(Comment entityToModify)
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

        public bool Delete(Comment entityToDelete)
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

        public Comment GetSingleAccountByKey(string key)
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

        public IEnumerable<Comment> GetAll()
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
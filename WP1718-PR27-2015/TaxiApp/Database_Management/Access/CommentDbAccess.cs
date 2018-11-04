using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaxiApp.Common;

namespace TaxiApp.Database_Management.Access
{
    public class CommentDbAccess : IDbAccess<CommentDbAccess, string>
    {
        public bool Add(CommentDbAccess entityToAdd)
        {
            throw new NotImplementedException();
        }

        public bool Delete(CommentDbAccess entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDbAccess> GetAll()
        {
            throw new NotImplementedException();
        }

        public CommentDbAccess GetSingleAccountByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Modify(CommentDbAccess entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
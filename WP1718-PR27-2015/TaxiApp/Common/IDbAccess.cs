using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Common
{
    public interface IDbAccess<T, Key>
    {
        bool Add(T entityToAdd);
        bool Modify(T entityToModify);
        bool Delete(T entityToDelete);
        T GetSingleAccountByKey(Key key);
        IEnumerable<T> GetAll();
    }
}

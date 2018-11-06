using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiApp.Common
{
    public abstract class BaseDbAccess<T, Key>
    {
        public abstract bool Add(T entityToAdd);
      
        public abstract bool Modify(T entityToModify);
        public abstract bool Delete(T entityToDelete);
        public abstract T GetSingleEntityByKey(Key key);
        public abstract IEnumerable<T> GetAll();
    }
}

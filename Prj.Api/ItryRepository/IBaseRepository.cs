using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItryRepositories.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        // GET api/T
        T[] Get();
        // GET api/T/5
        T Get(int id);
        // POST api/T
        void Insert(T obj);
        // PUT api/T/5
        void Update(int id, T obj);
        //// DELETE api/T/5
        void Delete(int id);

    }
}

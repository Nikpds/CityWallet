using Models;
using System.Collections.Generic;

namespace SqlContext
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        T Get(string id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(string id);
    }
}

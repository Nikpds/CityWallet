using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlContext
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(string id);

        Task<T> GetById(string id);

        Task<T> Insert(T entity);

        T Update(T entity);

        Task<bool> Delete(string id);
    }
}

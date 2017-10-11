using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlContext
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(string id);

        Task<T> Insert(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete(string id);
    }
}

using Models;

namespace SqlContext
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(string id);

        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}

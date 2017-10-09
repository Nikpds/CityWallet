using Models;
using System;

namespace SqlContext
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        public void Create(T entity)
        {

            //Write your logic here to persist the entity

        }

        public void Delete(T entity)
        {

            //Write your logic here to delete an entity

        }

        public T GetById(string id)
        {

            //Write your logic here to retrieve an entity by Id

            throw new NotImplementedException();

        }

        public void Update(T entity)
        {

            //Write your logic here to update an entity

        }
    }
}

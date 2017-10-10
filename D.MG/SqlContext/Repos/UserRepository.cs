using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlContext.Repos
{
    public class UserRepository : IRepository<User>
    {
        private DataContext _db;
        

        public UserRepository(DataContext context)
        {
            _db = context;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(User entity)
        {
            _db.Add(entity);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

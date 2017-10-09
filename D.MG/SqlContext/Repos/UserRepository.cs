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

        private DbSet<User> userEntity;

        public UserRepository(DataContext context)
        {
            _db = context;
            userEntity = context.Set<User>();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

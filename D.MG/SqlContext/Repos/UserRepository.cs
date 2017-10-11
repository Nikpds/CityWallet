using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class UserRepository : IRepository<User>
    {
        private DataContext _db;


        public UserRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<User> GetByUsername(string username)
        {
            var entity = await _db.Set<User>().FirstOrDefaultAsync(x => x.Vat == username);
            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _db.FindAsync<User>(id);
            _db.Remove(entity);
            return true;
        }

        public async Task<User> GetById(string id)
        {
            var entity = await _db.FindAsync<User>(id);
            return entity;
        }

        public async Task<User> Insert(User entity)
        {
            var user = await _db.AddAsync(entity);

            return user.Entity;
        }

        public bool InsertMany(List<User> entities)
        {
            _db.AddRange(entities);

            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _db.FindAsync<List<User>>();

            return users;
        }

        public async Task<User> Update(User entity)
        {
            var user = _db.Update(entity);

            return user.Entity;
        }

    }
}

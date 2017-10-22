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
        private DbSet<User> dbSet;

        public UserRepository(DataContext context)
        {
            dbSet = context.Set<User>();
        }

        public async Task<User> GetByUsername(string username)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Vat == username);

            return entity;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            return true;
        }

        public async Task<User> GetById(string id)
        {
            var entity = await dbSet.Include(x=>x.Address).SingleOrDefaultAsync(s=>s.Id == id);

            return entity;
        }

        public async Task<User> Insert(User entity)
        {
            var user = await dbSet.AddAsync(entity);

            return user.Entity;
        }

        public bool InsertMany(List<User> entities)
        {
            dbSet.AddRange(entities);

            return true;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await dbSet.ToListAsync();

            return users;
        }

        public User Update(User entity)
        {
            var user = dbSet.Update(entity);

            return user.Entity;
        }

        public Task<IEnumerable<User>> GetAll(string id)
        {
            throw new NotImplementedException();
        }
        
    }
}

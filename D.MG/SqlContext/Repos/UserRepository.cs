using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class UserRepository : IUserRepository
    {
        private DbSet<User> dbSet;
        private DataContext ctx;
        public UserRepository(DataContext context)
        {
            ctx = context;
            dbSet = ctx.Set<User>();
        }

        public async Task<User> GetByUsername(string username)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Vat == username);

            return entity;
        }

        public async Task<User> GetById(string id)
        {
            var entity = await dbSet.Include(x => x.Address).SingleOrDefaultAsync(s => s.Id == id);

            return entity;
        }

        public bool InsertMany(List<User> entities)
        {
            dbSet.AddRange(entities);

            return true;
        }

        public User Update(User entity)
        {
            var user = dbSet.Update(entity);

            return user.Entity;
        }

        public void Dispose()
        {
            ctx.Dispose();
        }

        public void Save()
        {
            ctx.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class DebtRepository : IRepository<Debt>
    {
        private DbSet<Debt> dbSet;

        public DebtRepository(DataContext context)
        {
            dbSet = context.Set<Debt>();
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            return true;
        }

        public async Task<Debt> GetById(string id)
        {
            var entity = await dbSet.FindAsync(id);

            return entity;
        }

        public async Task<Debt> Insert(Debt entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }
        
        public async Task<IEnumerable<Debt>> GetAll(string userId)
        {
            var result = await dbSet.Where(x=>x.UserId==userId).ToListAsync();

            return result;
        }

        public Debt Update(Debt entity)
        {
            var result = dbSet.Update(entity);

            return result.Entity;
        }

        public async Task<IEnumerable<Debt>> GetAll()
        {
            var result =await dbSet.ToListAsync();

            return result;
        }
    }
}

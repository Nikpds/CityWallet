using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class BillRepository : IRepository<Bill>
    {
        private DbSet<Bill> dbSet;

        public BillRepository(DataContext context)
        {
            dbSet = context.Set<Bill>();
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            return true;
        }

        public async Task<Bill> GetById(string id)
        {
            var entity = await dbSet.FindAsync(id);

            return entity;
        }

        public async Task<Bill> Insert(Bill entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }

        public bool UpdateMany(List<Bill> entities)
        {
            dbSet.UpdateRange(entities);

            return true;
        }

        public async Task<IEnumerable<Bill>> GetAll(string userId)
        {
            var result = await dbSet.Where(x=>x.UserId==userId).ToListAsync();

            return result;
        }

        public Bill Update(Bill entity)
        {
            var result = dbSet.Update(entity);

            return result.Entity;
        }

        public async Task<IEnumerable<Bill>> GetAll()
        {
            var result =await dbSet.ToListAsync();

            return result;
        }
    }
}

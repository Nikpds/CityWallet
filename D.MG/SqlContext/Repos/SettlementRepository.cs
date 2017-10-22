using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class SettlementRepository : ISettlementRepository
    {
        private DbSet<Settlement> dbSet;

        public SettlementRepository(DataContext context)
        {
            dbSet = context.Set<Settlement>();
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);

            dbSet.Remove(entity);

            return true;
        }

        public async Task<Settlement> GetById(string id)
        {
            var entity = await dbSet.FindAsync(id);

            return entity;
        }

        public async Task<Settlement> Insert(Settlement entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }

        public async Task<IEnumerable<Settlement>> GetAll()
        {
            var result = await dbSet.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Settlement>> GetAll(string id)
        {
            //var result = await dbSet.Where(x => x.UserId == userId).ToListAsync();
            var result = await dbSet.ToListAsync();

            return result;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class BillRepository : IBillRepository
    {
        private DbSet<Bill> dbSet;
        private DataContext ctx;
        public BillRepository(DataContext context)
        {
            dbSet = context.Set<Bill>();
            ctx = context;
        }


        public async Task<Bill> GetById(string id)
        {
            var entity = await dbSet.FindAsync(id);

            return entity;
        }

        public bool UpdateMany(List<Bill> entities)
        {
            dbSet.UpdateRange(entities);

            return true;
        }

        public async Task<IEnumerable<Bill>> GetAll(string userId)
        {
            var result = await dbSet.Where(x => x.UserId == userId).ToListAsync();

            return result;
        }

        public Bill Update(Bill entity)
        {
            var result = dbSet.Update(entity);

            return result.Entity;
        }


        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

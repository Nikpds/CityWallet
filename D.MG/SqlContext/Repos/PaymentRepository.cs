using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class PaymentRepository : IRepository<Payment>
    {
        private DbSet<Payment> dbSet;

        public PaymentRepository(DataContext context)
        {
            dbSet = context.Set<Payment>();
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
            return true;
        }

        public async Task<Payment> GetById(string id)
        {
            var entity = await dbSet.FindAsync(id);

            return entity;
        }

        public async Task<Payment> Insert(Payment entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            var result = await dbSet.ToListAsync();

            return result;
        }

        public Payment Update(Payment entity)
        {
            var result = dbSet.Update(entity);

            return result.Entity;
        }

        public async Task<IEnumerable<Payment>> GetAll(string id)
        {
            //var result = await dbSet.Where(x => x.UserId == id).ToListAsync();
            var result = await dbSet.ToListAsync();

            return result;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class PaymentRepository : IPaymentRepository
    {
        private DbSet<Payment> dbSet;

        public PaymentRepository(DataContext context)
        {
            dbSet = context.Set<Payment>();
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

        public bool InsertMany(List<Payment> entities)
        {
            dbSet.AddRange(entities);

            return true;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            var result = await dbSet.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Payment>> GetAll(string id)
        {
            var result = await dbSet.Where(x => x.Bill.UserId == id).ToListAsync();

            return result;
        }

        public bool UpdateMany(List<Payment> entities)
        {
            dbSet.UpdateRange(entities);

            return true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

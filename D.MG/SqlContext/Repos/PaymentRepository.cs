using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class PaymentRepository : IRepository<Payment>
    {
        private DataContext _db;
        
        public PaymentRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _db.FindAsync<Payment>(id);
            _db.Remove(entity);
            return true;
        }

        public async Task<Payment> GetById(string id)
        {
            var entity = await _db.FindAsync<Payment>(id);
            return entity;
        }

        public async Task<Payment> Insert(Payment entity)
        {
            var result = await _db.AddAsync(entity);

            return result.Entity;
        }

        public bool InsertMany(List<Payment> entities)
        {
            _db.AddRange(entities);

            return true;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            var result = await _db.FindAsync<List<Payment>>();

            return result;
        }

        public async Task<Payment> Update(Payment entity)
        {
            var result = _db.Update(entity);

            return result.Entity;
        }
    }
}

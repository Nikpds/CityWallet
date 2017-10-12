using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class DebtRepository : IRepository<Debt>
    {
        private DataContext _db;


        public DebtRepository(DataContext context)
        {
            _db = context;
        }

        public DebtRepository()
        {
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _db.FindAsync<Debt>(id);
            _db.Remove(entity);
            return true;
        }

        public async Task<Debt> GetById(string id)
        {
            var entity = await _db.FindAsync<Debt>(id);
            return entity;
        }

        public async Task<Debt> Insert(Debt entity)
        {
            var result = await _db.AddAsync(entity);

            return result.Entity;
        }
        

        public IEnumerable<Debt> GetAll(string userId)
        {
            var result = _db.Debt.Where(x => x.UserId == userId).ToList();

            return result;
        }

        public async Task<Debt> Update(Debt entity)
        {
            var result = _db.Update(entity);

            return result.Entity;
        }

        public Task<IEnumerable<Debt>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

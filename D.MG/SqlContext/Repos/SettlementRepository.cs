using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
   public class SettlementRepository:IRepository<Settlement>
    {
        private DataContext _db;


        public SettlementRepository(DataContext context)
        {
            _db = context;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _db.FindAsync<Settlement>(id);
            _db.Remove(entity);
            return true;
        }

        public async Task<Settlement> GetById(string id)
        {
            var entity = await _db.FindAsync<Settlement>(id);
            return entity;
        }

        public async Task<Settlement> Insert(Settlement entity)
        {
            var result = await _db.AddAsync(entity);

            return result.Entity;
        }

        public bool InsertMany(List<Settlement> entities)
        {
            _db.AddRange(entities);

            return true;
        }

        public async Task<IEnumerable<Settlement>> GetAll()
        {
            var result = await _db.FindAsync<List<Settlement>>();

            return result;
        }

        public async Task<Settlement> Update(Settlement entity)
        {
            var result = _db.Update(entity);

            return result.Entity;
        }
    }
}

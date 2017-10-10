using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlContext.Repos
{
    public class DebtRepository : IRepository<Debt>
    {
        private DataContext _db;

        public DebtRepository(DataContext context)
        {
            _db = context;
        }

        public void Delete(string id)
        {
            var entity = _db.Find<Debt>(id);
            _db.Remove(entity);
        }

        public Debt Get(string id)
        {
            return _db.Find<Debt>(id);
        }

        public async IEnumerable<Debt> GetAll()
        {
            return await _db.FindAsync<Debt>();
        }

        public void Insert(Debt entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Debt entity)
        {
            throw new NotImplementedException();
        }
    }
}

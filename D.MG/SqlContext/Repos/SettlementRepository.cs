using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlContext.Repos
{
   public class SettlementRepository:IRepository<Settlement>
    {
        private DataContext _db;

        public SettlementRepository(DataContext context)
        {
            _db = context;
        }

        public void Delete(Settlement entity)
        {
            throw new NotImplementedException();
        }

        public Settlement Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Settlement> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Settlement entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Settlement entity)
        {
            throw new NotImplementedException();
        }

       
    }
}

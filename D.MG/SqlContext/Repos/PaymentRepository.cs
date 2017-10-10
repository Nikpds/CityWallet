using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlContext.Repos
{
    public class PaymentRepository : IRepository<Payment>
    {
        private DataContext _db;
        
        public PaymentRepository(DataContext context)
        {
            _db = context;
        }

        public void Delete(Payment entity)
        {
            throw new NotImplementedException();
        }

        public Payment Get(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Payment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Payment entity)
        {
            throw new NotImplementedException();
        }
    }
}

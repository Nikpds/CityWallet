using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public class BillRepository : IRepository<Bill>
    {
        private DbSet<Bill> dbSet;

        public BillRepository(DataContext context)
        {
            dbSet = context.Set<Bill>();
        }

        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bill>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bill>> GetAll(string id)
        {
            var bills = await dbSet.Where(x => x.UserId == id).ToListAsync();

            return bills;
        }

        public Task<Bill> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> Insert(Bill entity)
        {
            throw new NotImplementedException();
        }

        public Bill Update(Bill entity)
        {
            throw new NotImplementedException();
        }
    }
}

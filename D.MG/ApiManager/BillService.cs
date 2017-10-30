using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager
{
    public class BillService
    {
        private DbSet<Bill> _dbSet;
        private DataContext _ctx;

        public BillService(DataContext ctx)
        {
            _dbSet = ctx.Set<Bill>();
            _ctx = ctx;
        }

        public async Task<IEnumerable<Bill>> GetUnpaidBills(string userId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.Payment == null && x.SettlementId == null).ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetPaidBills(string userId)
        {
            return await _dbSet.Include(i => i.Payment).Where(x => x.UserId == userId && x.Payment != null).ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsOnSettlement(string userId)
        {
            return await _dbSet.Include(i => i.Settlement).Where(x => x.UserId == userId && x.Settlement != null).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManager
{
    public interface IBillService
    {
        Task<IEnumerable<Bill>> GetUnpaidBills(string userId);
        Task<IEnumerable<Bill>> GetPaidBills(string userId);
        Task<IEnumerable<Bill>> GetBillsOnSettlement(string userId);
    }

    public class BillService : IBillService
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

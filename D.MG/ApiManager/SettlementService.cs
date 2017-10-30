using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager
{
    public class SettlementService
    {
        private DbSet<Settlement> _dbSet;
        private DataContext _ctx;

        public SettlementService(DataContext ctx)
        {
            _dbSet = ctx.Set<Settlement>();
            _ctx = ctx;
        }


        public async Task<Settlement> InsertSettlement(Settlement settle)
        {
            var bills = settle.Bills.ToList();
            settle.LastUpdate = DateTime.Now;
            settle.RequestDate = DateTime.Now;
            settle.Bills = new List<Bill>();

            _dbSet.Add(settle);

            bills.ForEach(x => x.SettlementId = settle.Id);

            _ctx.Set<Bill>().UpdateRange(bills);

            await _ctx.SaveChangesAsync();

            return settle;
        }

        public async Task<Boolean> CancelSettlement(string id)
        {
            var settlement = await _dbSet.Include(i => i.Bills).FirstAsync(x => x.Id == id);

            var bills = settlement.Bills.ToList();

            bills.ForEach(x => x.SettlementId = null);

            var deleted = _dbSet.Remove(settlement);

            _ctx.SaveChanges();

            return true;
        }

        public async Task<ICollection<Settlement>> GetUserSettlements(string userId)
        {
            var settlements = await _dbSet.Include(i=> i.Bills).Where(w => w.Bills.Any(b => b.UserId == userId)).ToArrayAsync();

            return settlements;
        }

        public async Task<Settlement> GetUserSettlement(string id)
        {
            var settlement = await _dbSet.FindAsync(id);

            return settlement;
        }


        public void CreateSettlementTypes()
        {
            var types = new List<SettlementType>()
            {
                new SettlementType(){ Downpayment=10,Installments=24,Interest=4.1},
                new SettlementType(){ Downpayment=20,Installments=24,Interest=3.9},
                new SettlementType(){ Downpayment=30,Installments=36,Interest=3.6},
                new SettlementType(){ Downpayment=40,Installments=36,Interest=3.2},
                new SettlementType(){ Downpayment=50,Installments=48,Interest=2.6}
            };

            _ctx.Set<SettlementType>().AddRange(types);

            _ctx.SaveChanges();

        }

        public async Task<ICollection<SettlementType>> GetSettlementTypes()
        {
            var types = await _ctx.Set<SettlementType>().ToListAsync();

            return types;
        }
    }
}

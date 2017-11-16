using ApiManager.Models;
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
    public interface ISettlementService
    {
        Task<SettlementDto> InsertSettlement(SettlementDto settle);
        Task<Boolean> CancelSettlement(string id, string userId);
        Task<IEnumerable<SettlementDto>> GetUserSettlements(string userId);
        Task<SettlementDto> GetUserSettlement(string userId, string id);
        Task<ICollection<SettlementType>> GetSettlementTypes();
    }

    public class SettlementService : ISettlementService
    {
        private DbSet<Settlement> _dbSet;
        private DataContext _ctx;

        public SettlementService(DataContext ctx)
        {
            _dbSet = ctx.Set<Settlement>();
            _ctx = ctx;
        }


        public async Task<SettlementDto> InsertSettlement(SettlementDto settle)
        {
            var sType = await _ctx.Set<SettlementType>().FirstOrDefaultAsync(f => f.Id == settle.SettlementType.Id);
            settle.SettlementType = sType;

            var settlement = new SettlementDto().ToDomainModel(settle);
            var bills = settlement.Bills.ToList();

            settlement.Downpayment = settlement.Bills.Sum(s => s.Amount) * (settlement.SettlementType.Downpayment / 100);
            settlement.LastUpdate = DateTime.Now;
            settlement.RequestDate = DateTime.Now;
            settle.Bills = new List<Bill>();

            _dbSet.Add(settlement);

            bills.ForEach(x => x.SettlementId = settlement.Id);

            _ctx.Set<Bill>().UpdateRange(bills);

            _ctx.SaveChanges();

            return new SettlementDto(settlement);
        }

        public async Task<Boolean> CancelSettlement(string id, string userId)
        {
            var settlement = await _dbSet.Include(i => i.Bills).SingleOrDefaultAsync(x => x.Id == id && x.Bills.Any(a => a.UserId == userId));

            if (settlement == null)
            {
                return false;
            }

            var bills = settlement.Bills.ToList();

            bills.ForEach(x => x.SettlementId = null);

            var deleted = _dbSet.Remove(settlement);

            _ctx.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<SettlementDto>> GetUserSettlements(string userId)
        {
            var settlements = await _dbSet.Include(i => i.Bills).Include(x => x.SettlementType).Where(w => w.Bills.Any(b => b.UserId == userId)).ToArrayAsync();
            var dtos = settlements.Select(s => new SettlementDto(s));

            return dtos;
        }

        public async Task<SettlementDto> GetUserSettlement(string userId, string id)
        {
            var settlement = await _dbSet.Include(i => i.Bills).Include(s=>s.SettlementType).SingleOrDefaultAsync(x => x.Id == id && x.Bills.Any(a => a.UserId == userId));

            return new SettlementDto(settlement);
        }


        public void CreateSettlementTypes()
        {
            var types = new List<SettlementType>()
            {
                new SettlementType(){ Downpayment=10,Installments=24,Interest=4.1m},
                new SettlementType(){ Downpayment=20,Installments=24,Interest=3.9m},
                new SettlementType(){ Downpayment=30,Installments=36,Interest=3.6m},
                new SettlementType(){ Downpayment=40,Installments=36,Interest=3.2m},
                new SettlementType(){ Downpayment=50,Installments=48,Interest=2.6m}
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

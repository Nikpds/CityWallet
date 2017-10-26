using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public interface IBillRepository : IDisposable
    {
        Task<Bill> GetById(string id);

        bool UpdateMany(List<Bill> entities);

        Task<IEnumerable<Bill>> GetUnpaidBills(string userId);

        Bill Update(Bill entity);

        Task<IEnumerable<Bill>> GetBillsOnSettlement(string userId);

        Task<IEnumerable<Bill>> GetPaidBills(string userId);
    }
}

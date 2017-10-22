using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public interface ISettlementRepository : IDisposable
    {
        Task<IEnumerable<Settlement>> GetAll(string id);
        Task<IEnumerable<Settlement>> GetAll();
        Task<Settlement> Insert(Settlement entity);
        Task<Settlement> GetById(string id);
        Task<bool> Delete(string id);
    }
}

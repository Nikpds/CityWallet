using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public interface IPaymentRepository : IDisposable
    {
        Task<Payment> GetById(string id);

        Task<Payment> Insert(Payment entity);

        bool InsertMany(List<Payment> entities);

        Task<IEnumerable<Payment>> GetAll();

        Task<IEnumerable<Payment>> GetAll(string id);
    }
}

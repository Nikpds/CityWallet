using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManager
{
    public interface IPaymentService
    {
        List<Payment> Payments(List<Bill> bills);
        Task<IEnumerable<Payment>> GetPayments(string userId);
    }

    public class PaymentService: IPaymentService
    {
        private DbSet<Payment> _dbSet;
        private DataContext _ctx;

        public PaymentService(DataContext ctx)
        {
            _dbSet = ctx.Set<Payment>();
            _ctx = ctx;
        }

        public List<Payment> Payments(List<Bill> bills)
        {
            var payments = new List<Payment>();
            for (var i = 0; i < bills.Count; i++)
            {
                var payment = new Payment
                {
                    Bill_Id = bills[i].Bill_Id,
                    Method = "Debit",
                    PaidDate = DateTime.Now,
                    BillId = bills[i].Id
                };
                payments.Add(payment);
            }
            _dbSet.AddRange(payments);

            _ctx.SaveChanges();

            return payments;
        }

        public async Task<IEnumerable<Payment>> GetPayments(string userId)
        {
            return await _dbSet.Include(i => i.Bill).Where(x => x.Bill.UserId == userId).ToListAsync();
        }

    }
}

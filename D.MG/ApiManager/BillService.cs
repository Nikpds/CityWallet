using Models;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager
{
    public class BillService
    {
        private IPaymentRepository _pays;
        private IBillRepository _bills;

        public BillService(IPaymentRepository pays, IBillRepository bills)
        {
            _pays = pays;
            _bills = bills;
        }

        public List<Bill> PayBills(List<Bill> bills)
        {

            for (var i = 0; i < bills.Count; i++)
            {
                bills[i].Payment = new Payment
                {
                    Bill_Id = bills[i].Bill_Id,
                    Method = "Debit",
                    PaidDate = DateTime.Now
                };

            }
            return bills;
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
            var success = _pays.InsertMany(payments);

            return success ? payments : new List<Payment>();
        }

        public async Task<IEnumerable<Bill>> GetUnpaidBills(string id)
        {
            return  await _bills.GetUnpaidBills(id);
        }

        public async Task<IEnumerable<Payment>> GetPayments(string id)
        {
            return await _pays.GetUserPayments(id);
        }
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager
{
    public class BillManager
    {

        public static List<Bill> PayBills(List<Bill> bills)
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


        public static List<Payment> Payments(List<Bill> bills)
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
            return payments;
        }
    }
}

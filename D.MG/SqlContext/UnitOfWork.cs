using Microsoft.EntityFrameworkCore;
using SqlContext.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlContext
{
    public class UnitOfWork : IDisposable
    {
        private DataContext context;

        private UserRepository userRepository;
        private PaymentRepository paymentRepository;
        private SettlementRepository settlementRepository;
        private BillRepository debtRepository;

        public UnitOfWork(DataContext _context)
        {
            this.context = _context;
        }

        public UserRepository UserRepository
        {
            get { return userRepository ?? new UserRepository(context); }
        }

        public PaymentRepository PaymentRepository
        {
            get { return paymentRepository ?? new PaymentRepository(context); }
        }

        public SettlementRepository SettlementRepository
        {
            get { return settlementRepository ?? new SettlementRepository(context); }
        }

        public BillRepository BillRepository
        {
            get { return debtRepository ?? new BillRepository(context); }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Models;

namespace SqlContext
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new PaymentMap(modelBuilder.Entity<Payment>());
            new BillMap(modelBuilder.Entity<Bill>());
            new SettlementMap(modelBuilder.Entity<Settlement>());
        }
    }
}

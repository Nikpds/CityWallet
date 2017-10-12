using Microsoft.EntityFrameworkCore;
using Models;

namespace SqlContext
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Debt> Debt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new PaymentMap(modelBuilder.Entity<Payment>());
            new DebtMap(modelBuilder.Entity<Debt>());
            new SettlementMap(modelBuilder.Entity<Settlement>());
        }
    }
}

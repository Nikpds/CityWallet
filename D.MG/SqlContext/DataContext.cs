
using Microsoft.EntityFrameworkCore;
using Models;
using Z.EntityFramework.Extensions;

namespace SqlContext
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userBuilder = modelBuilder.Entity<User>();

            userBuilder.HasKey(x => x.Id);
            userBuilder.Property(x => x.Vat)
                       .IsRequired()
                       .HasAnnotation("unique", true);
            userBuilder.Property(x => x.Name)
                       .IsRequired();
            userBuilder.Property(x => x.Lastname)
                       .IsRequired();
            userBuilder.Property(x => x.Password)
                       .IsRequired();
            userBuilder.Property(x => x.Email)
                       .IsRequired()
                       .HasAnnotation("unique", true);
            userBuilder.HasMany(x => x.Bills)
                       .WithOne(x => x.User)
                       .HasForeignKey(x => x.UserId)
                       .IsRequired();
            userBuilder.HasOne(x => x.AddressInfo)
                       .WithOne(u => u.User)
                       .HasForeignKey<Address>(b => b.UserId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);

            var billBuilder = modelBuilder.Entity<Bill>();

            billBuilder.HasKey(t => t.Id);
            billBuilder.Property(t => t.Bill_Id)
                       .IsRequired()
                       .HasAnnotation("unique", true);
            billBuilder.Property(t => t.Amount)
                       .IsRequired();
            billBuilder.Property(t => t.DateDue)
                       .IsRequired();
            billBuilder.Property(t => t.Description)
                       .IsRequired();
            billBuilder.HasOne(x => x.Payment)
                       .WithOne(b => b.Bill)
                       .HasForeignKey<Payment>(b => b.BillId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Cascade);


            var paymentBuilder = modelBuilder.Entity<Payment>();

            paymentBuilder.HasKey(t => t.Id);
            paymentBuilder.Property(t => t.Method)
                          .IsRequired();


            var addressBuilder = modelBuilder.Entity<Address>();

            addressBuilder.Property(x => x.Street)
                          .IsRequired();
            addressBuilder.Property(x => x.County)
                          .IsRequired();

            var settlementBuilder = modelBuilder.Entity<Settlement>();

            settlementBuilder.HasKey(t => t.Id);
            settlementBuilder.Property(t => t.Installments)
                             .IsRequired();
            settlementBuilder.Property(t => t.Downpayment)
                             .IsRequired();
            settlementBuilder.Property(t => t.RequestDate)
                             .IsRequired();
            settlementBuilder.HasMany(x => x.Bills)
                             .WithOne(w => w.Settlement)
                             .HasForeignKey(h => h.SettlementId);

            var settlementTypeBuilder = modelBuilder.Entity<SettlementType>();

            settlementTypeBuilder.HasMany(x => x.Settlements)
                                 .WithOne(x => x.SettlementType)
                                 .HasForeignKey(x => x.SettlementTypeId)
                                 .IsRequired();
        }
    }
}

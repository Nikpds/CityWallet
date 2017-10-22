
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Models
{
    public class Bill : BaseEntity
    {
        public string Description { get; set; }
        public string Bill_Id { get; set; }
        public double Amount { get; set; }
        
        public  Payment Payment { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }

        public DateTime DateDue { get; set; }
    }

    public class BillMap
    {
        public BillMap(EntityTypeBuilder<Bill> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Bill_Id)
                         .IsRequired()
                         .HasAnnotation("unique", true);
            entityBuilder.Property(t => t.Amount)
                         .IsRequired();
            entityBuilder.Property(t => t.DateDue)
                         .IsRequired();
            entityBuilder.Property(t => t.Description)
                         .IsRequired();
            entityBuilder.HasOne(x => x.Payment)
                         .WithOne(b => b.Bill)
                         .HasForeignKey<Payment>(b => b.BillId)
                         .IsRequired()
                         .OnDelete(DeleteBehavior.Cascade);

        }
    }

}


using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Models
{
    public class Debt : EntityBase
    {
        public string Description { get; set; }
        public string BillId { get; set; }
        public double Amount { get; set; }

        public string PaymentId { get; set; }
        public Payment Payment { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime DateDue { get; set; }
    }

    public class DebtMap
    {
        public DebtMap(EntityTypeBuilder<Debt> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.BillId)
                         .IsRequired()
                         .HasAnnotation("unique", true);
            entityBuilder.Property(t => t.Amount)
                         .IsRequired();
            entityBuilder.Property(t => t.DateDue)
                         .IsRequired();
            entityBuilder.Property(t => t.Description)
                         .IsRequired();

        }
    }

}

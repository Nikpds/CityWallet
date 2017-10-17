using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Models
{
    public class Payment : EntityBase
    {
        public string BILL_ID { get; set; }
        public DateTime Time { get; set; }
        public string Method { get; set; }
        public Debt Debt { get; set; }
    }

    public class PaymentMap
    {
        public PaymentMap(EntityTypeBuilder<Payment> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.BILL_ID)
                         .IsRequired();
            entityBuilder.Property(t => t.Method)
                         .IsRequired();
            entityBuilder.HasOne(x => x.Debt)
                         .WithOne(b => b.Payment)
                         .HasForeignKey<Debt>(b => b.PaymentId); ;
        }
    }
}

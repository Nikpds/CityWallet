using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Models
{
    public class Payment : BaseEntity
    {
        public string Bill_Id { get; set; }

        public DateTime PaidDate { get; set; }

        public string Method { get; set; }

        public string BillId { get; set; }
        public virtual Bill Bill { get; set; }
    }

    public class PaymentMap
    {
        public PaymentMap(EntityTypeBuilder<Payment> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Bill_Id)
                         .IsRequired();
            entityBuilder.Property(t => t.Method)
                         .IsRequired();
        }
    }
}

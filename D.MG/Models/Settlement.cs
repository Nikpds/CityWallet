using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Settlement : BaseEntity
    {
        public double Interest { get; set; }
        public int Installments { get; set; }
        public double Downpayment { get; set; }
        public DateTime RequestDate { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }

        public Settlement()
        {
            Bills = new HashSet<Bill>();
        }
    }

    public class SettlementMap
    {
        public SettlementMap(EntityTypeBuilder<Settlement> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Interest)
                         .IsRequired();
            entityBuilder.Property(t => t.Installments)
                         .IsRequired();
            entityBuilder.Property(t => t.Downpayment)
                         .IsRequired();
            entityBuilder.Property(t => t.RequestDate)
                         .IsRequired();
            entityBuilder.HasMany(x => x.Bills)
                         .WithOne(w => w.Settlement)
                         .HasForeignKey(h => h.SettlementId);
        }

    }
}

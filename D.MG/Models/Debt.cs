using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Models
{
    public class Debt : EntityBase
    {
        public string Vat { get; set; }
        public string Description { get; set; }
        public string BillId { get; set; }
        public double Amount { get; set; }

        public DateTime DateDue { get; set; }
    }

    public class DebtMap
    {
        public DebtMap(EntityTypeBuilder<Debt> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }

}

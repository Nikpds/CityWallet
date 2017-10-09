using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class Payment : EntityBase
    {

    }

    public class PaymentMap
    {
        public PaymentMap(EntityTypeBuilder<Payment> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }
}

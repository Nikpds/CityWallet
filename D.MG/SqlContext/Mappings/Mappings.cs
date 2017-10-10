using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace SqlContext.Mappings
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }

    public class SettlementMap
    {
        public SettlementMap(EntityTypeBuilder<Settlement> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }

    public class PaymentMap
    {
        public PaymentMap(EntityTypeBuilder<Payment> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }

    public class DebtMap
    {
        public DebtMap(EntityTypeBuilder<Debt> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }


}

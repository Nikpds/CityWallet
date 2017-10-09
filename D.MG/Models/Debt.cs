using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class Debt : EntityBase
    {

    }

    public class DebtMap
    {
        public DebtMap(EntityTypeBuilder<Debt> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }

}

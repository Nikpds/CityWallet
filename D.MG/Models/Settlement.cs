using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class Settlement : EntityBase
    {

    }

    public class SettlementMap
    {
        public SettlementMap(EntityTypeBuilder<Settlement> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }
}

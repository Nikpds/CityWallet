using System.Collections.Generic;

namespace Models
{
    public class SettlementType : BaseEntity
    {
        public decimal Downpayment { get; set; }
        public int Installments { get; set; }
        public decimal Interest { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }

        public SettlementType()
        {
            Settlements = new HashSet<Settlement>();
        }
    }
}

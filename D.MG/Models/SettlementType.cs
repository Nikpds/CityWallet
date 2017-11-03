using System.Collections.Generic;

namespace Models
{
    public class SettlementType : BaseEntity
    {
        public double Downpayment { get; set; }
        public int Installments { get; set; }
        public double Interest { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }

        public SettlementType()
        {
            Settlements = new HashSet<Settlement>();
        }
    }
}

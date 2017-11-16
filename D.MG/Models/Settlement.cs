using System;
using System.Collections.Generic;

namespace Models
{
    public class Settlement : BaseEntity
    {
        public string Title { get; set; }
        public int Installments { get; set; }
        public decimal Downpayment { get; set; }
        public DateTime RequestDate { get; set; }

        public string SettlementTypeId { get; set; }
        public SettlementType SettlementType { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }

        public Settlement()
        {
            Bills = new HashSet<Bill>();
        }
    }
}

using System;

namespace Models
{
    public class Bill : BaseEntity
    {
        public string Description { get; set; }
        public string Bill_Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateDue { get; set; }

        public virtual Payment Payment { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string SettlementId { get; set; }
        public virtual Settlement Settlement { get; set; }

    }

}


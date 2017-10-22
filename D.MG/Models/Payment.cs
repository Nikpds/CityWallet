using System;

namespace Models
{
    public class Payment : BaseEntity
    {
        public string Bill_Id { get; set; }

        public DateTime PaidDate { get; set; }

        public string Method { get; set; }

        public string BillId { get; set; }
        public virtual Bill Bill { get; set; }
    }
    
}

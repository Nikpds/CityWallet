namespace ApiManager.Models
{
    public class Counter
    {
        public int Bills { get; set; }
        public decimal BillsAmount { get; set; }
        public int Paid { get; set; }
        public decimal PaidAmount { get; set; }
        public int UnPaid { get; set; }
        public decimal UnPaidAmount { get; set; }
        public int OnSettle { get; set; }
        public decimal OnSettleAmount { get; set; }
        public int Settlements { get; set; }
        public decimal SettleAmount { get; set; }
        public int Payments { get; set; }
        public decimal PaymentsAmount { get; set; }
    }
}

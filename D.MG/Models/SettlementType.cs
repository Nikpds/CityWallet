namespace Models
{
    public class SettlementType : BaseEntity
    {
        public double Downpayment { get; set; }
        public int Installments { get; set; }
        public double Interest { get; set; }
    }
}

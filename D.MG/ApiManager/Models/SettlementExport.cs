using System;
using System.Collections.Generic;
using System.Text;

namespace DMG.Services.Models
{
    public class SettlementExport
    {
        public string VAT { get; set; }
        public DateTime TIME { get; set; }
        public string BILLS { get; set; }
        public decimal DOWNPAYMENT { get; set; }
        public int INSTALLMENTS { get; set; }
        public decimal INTEREST { get; set; }
    }
}

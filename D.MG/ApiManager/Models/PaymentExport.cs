using System;
using System.Collections.Generic;
using System.Text;

namespace DMG.Services.Models
{
    public class PaymentExport
    {
        public string BILL_ID { get; set; }
        public DateTime TIME { get; set; }
        public decimal AMOUNT { get; set; }
        public string METHOD { get; set; }
        
    }
}

using System;

namespace DMG.Web.Api
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Owner { get; set; }
        public string Cvv { get; set; }
        public DateTime Expires { get; set; }
    }
}

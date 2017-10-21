using System;

namespace WebApp.Models
{
    public class CrediCard
    {
        public string CardNumber { get; set; }
        public string Owner { get; set; }
        public string CVV { get; set; }
        public DateTime Expires { get; set; }
    }
}

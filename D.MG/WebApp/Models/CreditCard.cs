using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string Owner { get; set; }
        public string Cvv { get; set; }
        public DateTime Expires { get; set; }
    }
}

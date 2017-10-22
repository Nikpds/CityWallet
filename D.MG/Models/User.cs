using System.Collections.Generic;

namespace Models
{
    public class User : BaseEntity
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            Address = new Address();
        }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Vat { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
  
}

using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiManager.Models
{
    public class UserDto
    {

        public UserDto()
        {
        }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Vat { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool PasswordReset { get; set; }
        public Counter Counters { get; set; }
        public string County { get; set; }
        public string Address { get; set; }
       
        public ICollection<Bill> Bills { get; set; }

        public UserDto(User user)
        {
            Name = user.Name;
            Lastname = user.Lastname;
            Vat = user.Vat;
            Mobile = user.Mobile;
            Email = user.Email;
            PasswordReset = user.PasswordReset;
            County = user.Address.County;
            Address = user.Address.Street;
            Bills = user.Bills;
        }
    }
}

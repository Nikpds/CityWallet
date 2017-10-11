using System;
using System.Collections.Generic;
using System.Text;

namespace AuthProvider
{
    public class PasswordReset
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}

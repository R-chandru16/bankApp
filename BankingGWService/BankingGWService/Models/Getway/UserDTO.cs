using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Models
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string JwtToken { get; set; }
    }
}

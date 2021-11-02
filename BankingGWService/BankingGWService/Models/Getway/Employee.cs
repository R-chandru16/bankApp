using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Models
{
    public class Employee
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Possition { get; set; }
        public string Credentials { get; set; }
        public string Role { get; set; }
    }
}

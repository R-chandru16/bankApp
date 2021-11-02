using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models
{
    public class Customer
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string DOB { get; set; }
        public string PAN_no { get; set; }
    }
}

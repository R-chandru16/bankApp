using BankingGWService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Models
{
    public class CustomerCreationStatusDTO
    { 
        public int StatusId { get; set; }
        public string CustomerEmail { get; set; }
        public string Message { get; set; }

        public CustomerDTO Customer { get; set; }
    }
}

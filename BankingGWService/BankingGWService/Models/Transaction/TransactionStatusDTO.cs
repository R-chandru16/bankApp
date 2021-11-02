using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Models
{
    public class TransactionStatusDTO
    {
        public string Message { get; set; }
        public float SourceBalance { get; set; }
        public float DestinationBalance { get; set; }
    }
}

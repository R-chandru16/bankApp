using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountServicesAPI.Models
{
    public class TransactionDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public int SourceAccountId { get; set; }
        public int TargerAccountId { get; set; }
        public int Amount { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime Date { get; set; }
    }
}

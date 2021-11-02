using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsMicroservice.Models
{
    public class Transaction
    {
        [Key]
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionsMicroservice.Models;
using TransactionsMicroservice.Services;

namespace TransactionsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _service;
        public TransactionController(TransactionService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public IEnumerable<Transaction> Get(int id)
        {
            var trans = _service.GetTransactions(id);
            return trans;
        }
        [Route("Deposit")]
        [HttpPost]
        public async Task<ActionResult<string>> Deposit(int accountId, int amount)
        {
            string result = _service.Deposit(accountId, amount);
            return result;

        }
        [Route("Withdraw")]
        [HttpPost]
        public async Task<ActionResult<string>> Withdraw(int accountId, int amount) 
        {
            string result = _service.Withdraw(accountId, amount);
            return result;

        }
        [Route("Transfer")]
        [HttpPost]
        public async Task<ActionResult<string>> Transfer(int sourceAccountId, int targetAccountId, int amount)
        {
            string result = _service.Transfer(sourceAccountId, targetAccountId, amount);
            return result;
        }
    }
}

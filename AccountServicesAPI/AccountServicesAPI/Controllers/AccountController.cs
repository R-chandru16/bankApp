using AccountServicesAPI.Models;
using AccountServicesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountServicesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }
        //// GET: api/<AccountController>
        //[HttpGet("AllAccounts")]
        //public IEnumerable<Account> AllAccounts()
        //{
        //    return _service.AllAccounts();
        //}

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Account GetAccount(int id)
        {
            return _service.GetAccount(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public AccountCreationStatus Post([FromBody] Account account)
        {
            AccountCreationStatus status = new();
            var acc = _service.AddAccount(account);
            if(acc != null)
            {
                status.AccountID = acc.AccountID;
                status.AccountStatus = "Success";
            }
            else
            {
                status.AccountID = 0;
                status.AccountStatus = "Failed";
            }
            return status;
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public Account Put(int id, [FromBody] Account account)
        {
            var acc = _service.EditAccount(id, account);
            return acc;
        }

        //// DELETE api/<AccountController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _service.RemoveAccount(id);
        //}

        //[Route("GetCustomerAccounts")]
        [HttpGet("GetCustomerAccounts/{id}")]
        public List<Account> GetCustomerAccounts(string id)
        {
            return _service.GetAccountsByCustID(id);
        }
        [HttpGet("Transactions")]
        public IEnumerable<TransactionDTO> Transactions(int AccId, DateTime FromDate,DateTime ToDate)
        {
            return _service.GetStatements(AccId,FromDate,ToDate);
        }
        [HttpPost("Withdraw")]
        public TransactionStatus Withdraw(int AccId,int Amount)
        {
            return _service.Withdraw(AccId, Amount);
        }
        [HttpPost("Deposit")]
        public TransactionStatus Deposit(int AccId, int Amount)
        {
            return _service.Deposit(AccId, Amount);
        }
        [HttpPost("Transfer")]
        public TransactionStatus Transfer(int FromAccId,int ToAccId,int Amount)
        {
            return _service.Transfer(FromAccId,ToAccId, Amount);
        }
    }
}

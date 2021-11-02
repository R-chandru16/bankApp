using BankingGWService.Models;
using BankingGWService.Models.Account;
using BankingGWService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingGWService.Controllers
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
        [HttpGet("AllAccounts")]
        public async Task<IEnumerable<AccountDTO>> AllAccounts(string id)
        {
            return await _service.GetAccountsByCustIDAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<AccountDTO> GetAccount(int id)
        {
            return await _service.GetAccount(id);
        }

        [HttpPost]
        public async Task<AccountCreationStatus> Post([FromBody] AccountDTO account)
        {
            return  await _service.AddAccountAsync(account);
        }

        [HttpGet("GetCustomerAccounts/{id}")]
        public async Task<List<AccountDTO>> GetCustomerAccounts(string id)
        {
            return await _service.GetAccountsByCustIDAsync(id);
        }

        [HttpGet("Transactions")]
        public async Task<IEnumerable<StatementDTO>> Transactions(int AccId, DateTime FromDate, DateTime ToDate)
        {
            return await _service.GetStatements(AccId, FromDate, ToDate);
        }

        [HttpPost("Withdraw")]
        public async Task<TransactionStatusDTO> Withdraw(int AccId, int Amount)
        {
            return await _service.Withdraw(AccId, Amount);
        }

        [HttpPost("Deposit")]
        public async Task<TransactionStatusDTO> Deposit(int AccId, int Amount)
        {
            return await _service.Deposit(AccId, Amount);
        }

        [HttpPost("Transfer")]
        public async Task<TransactionStatusDTO> Transfer(int FromAccId, int ToAccId, int Amount)
        {
            return await _service.Transfer(FromAccId, ToAccId, Amount);
        }
    }
}

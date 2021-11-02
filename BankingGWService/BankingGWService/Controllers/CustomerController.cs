using BankingGWService.Models;
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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _servise;

        public CustomerController(CustomerService service)
        {
            _servise = service;
        }

        [HttpGet("/getCustomerDetails/{id}")]
        public async Task<CustomerDTO> Get(string id, string token)
        {
            return await _servise.Get(id, token);
        }

        [HttpPost("/createCustomer")]
        public async Task<CustomerCreationStatusDTO> Post(CustomerDTO customer, string token)
        {
            return await _servise.Post(customer, token);
        }
    }
}

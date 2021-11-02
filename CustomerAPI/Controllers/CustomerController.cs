using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet("getCustomerDetails/{email}")]
        public Customer Get(string email)
        {
            return _service.get(email);
        }

        [HttpPost, Route("createCustomer")]
        public async Task<ActionResult<CustomerCreationStatus>> Post([FromBody] Customer order)
        {
            CustomerCreationStatus customer = _service.Add(order);
            if (customer != null)
            {
                return customer;
            }
            return BadRequest("Couldnt Add");
        }

    }
}

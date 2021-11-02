using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RulesMicroservice.Models;
using RulesMicroservice.Services;

namespace RulesMicroservice.Controllers
{
    [Route("api")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly RulesService _service;

        public RulesController(RulesService service)
        {
            _service = service;
        }
        [Route("GetRules")]
        [HttpGet]
        public Task<string> GetRules(int balance, int accountId)
        {
            var rule = _service.EvaluateMinBal(balance, accountId);
            return rule;
        }
        [Route("GetCharges")]
        [HttpGet]
        public Task<float> GetCharges(int balance, int accountId)
        {
            var charge = _service.GetServiceCharges(balance, accountId);
            return charge;
        }

    }
}

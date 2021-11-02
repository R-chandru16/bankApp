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
    public class RulesController : ControllerBase
    {
        private readonly RuleService _service;

        public RulesController(RuleService service)
        {
            _service = service;
        }
        [Route("GetRules")]
        [HttpGet]
        public async Task<string> GetRules(int balance, int accountId)
        {
            var rule = await _service.EvaluateMinBal(balance, accountId);
            return rule;
        }
        [Route("GetCharges")]
        [HttpGet]
        public async Task<float> GetCharges(int balance, int accountId)
        {
            var charge = await _service.GetServiceCharges(balance, accountId);
            return charge;
        }

    }
}
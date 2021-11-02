using Microsoft.EntityFrameworkCore;
using RulesMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RulesMicroservice.Services
{
    public class RulesService
    {
        public async Task<string> EvaluateMinBal(int balance, int accountId)
        {
            var account = new AccountDTO();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41001/api/");
            var getTask = client.GetAsync("Account/" + accountId);
            try
            {
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    account = await result.Content.ReadFromJsonAsync<AccountDTO>();
                    if (account.Balance > balance)
                    {
                        return "Allowed";
                    }
                    else
                    {
                        return "Denied. Not enough funds for a transaction";
                    }
                }
            }
            catch (DbUpdateConcurrencyException Dbce)
            {
                Console.WriteLine(Dbce.Message);
            }
            catch (DbUpdateException Dbe)
            {
                Console.WriteLine(Dbe.Message);
            }
            
            return "NA";
        }


        public async Task<float> GetServiceCharges(int balance, int accountId)
        {
            var account = new AccountDTO();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41001/api/");
            var getTask = client.GetAsync("Account/" + accountId);
            getTask.Wait();
            var result = getTask.Result;
            if (result.IsSuccessStatusCode)
            {
                account = await result.Content.ReadFromJsonAsync<AccountDTO>();
                if (account.Balance < balance)
                {
                    var charge = (float)(balance * 0.05f);
                    return charge;
                }
            }
            return 0;
        }
    }
}

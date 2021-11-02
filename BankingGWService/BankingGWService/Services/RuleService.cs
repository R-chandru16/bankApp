using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankingGWService.Services
{
    public class RuleService
    {
        public async Task<string> EvaluateMinBal(int balance, int accountId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41003/api/rule/");
            var getTask = await client.GetAsync($"/evaluateMinBal?accountId={accountId}&balance={balance}");
            if (getTask.IsSuccessStatusCode)
            {
                return await getTask.Content.ReadFromJsonAsync<string>();
            }
            return "";
        }

        public async Task<float> GetServiceCharges(int balance, int accountId)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41003/api/rule/");
            var getTask = await client.GetAsync("getServiceCharges/" + accountId);
            if (getTask.IsSuccessStatusCode)
            {
                return await getTask.Content.ReadFromJsonAsync<float>();
            }
            return 0;
        }
    }
}
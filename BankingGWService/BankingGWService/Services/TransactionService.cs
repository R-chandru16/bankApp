using BankingGWService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BankingGWService.Services
{
    public class TransactionService
    {
        public async Task<IEnumerable<TransactionDTO>> GetTransactions(int id)
        {
            List<TransactionDTO> transactions = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:41002/api/transaction/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                var getTask = await client.GetAsync("/getTransactions" + id);
                if (getTask.IsSuccessStatusCode)
                {
                    transactions = await getTask.Content.ReadFromJsonAsync<List<TransactionDTO>>();
                }
            }
            return transactions;
        }

        public async Task<string> Deposit(int accountId, int amount)
        {
            string status = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:41002/api/transaction/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                var getTask = await client.GetAsync($"/getTransactions?accountId={accountId}&amount={amount}");
                if (getTask.IsSuccessStatusCode)
                {
                    status = await getTask.Content.ReadFromJsonAsync<string>();
                }
            }
            return status;
        }


        public async Task<string> Withdraw(int accountId, int amount)
        {
            string status = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:41002/api/transaction/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                var getTask = await client.GetAsync($"/getTransactions?accountId={accountId}&amount={amount}");
                if (getTask.IsSuccessStatusCode)
                {
                    status = await getTask.Content.ReadFromJsonAsync<string>();
                }
            }
            return status;
        }

        public async Task<string> Transfer(int sourceAccountId, int targerAccountId, int amount)
        {
            string status = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:41002/api/transaction/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                var getTask = await client.GetAsync($"/getTransactions?sourceAccountId={sourceAccountId}&targerAccountId={targerAccountId}&amount={amount}");
                if (getTask.IsSuccessStatusCode)
                {
                    status = await getTask.Content.ReadFromJsonAsync<string>();
                }
            }
            return status;
        }
    }
}
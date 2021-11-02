using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TransactionsMicroservice.Models;

namespace TransactionsMicroservice.Services
{
    public class TransactionService
    {
        private readonly TransactionContext _context;
        public TransactionService(TransactionContext context)
        {
            _context = context;
        }

        public List<Transaction> GetTransactions(int id)
        {
            List<Transaction> transactions;
            try
            {
                transactions = _context.Transactions.Where(t => t.AccountId == id).ToList();
                return transactions;
            }
            catch (DbUpdateConcurrencyException Dbce)
            {
                Console.WriteLine(Dbce.Message);
            }
            catch (DbUpdateException Dbe)
            {
                Console.WriteLine(Dbe.Message);
            }
            return null;
        }

        public string Deposit(int accountId, int amount)
        {
            string status = "";
            Transaction transaction = new Transaction();
            AccountDTO account = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41001/api/");
            var rules = new HttpClient();
            rules.BaseAddress = new Uri("http://localhost:41003/api/");


            try
            {
                var getTask = client.GetAsync("Account/" + accountId);
                getTask.Wait();
                var result = getTask.Result;

                var getCharges = rules.GetAsync($"GetCharges?balance={amount}&accountId={accountId}");
                getCharges.Wait();
                var resRules = getCharges.Result;
                if (result.IsSuccessStatusCode & resRules.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadFromJsonAsync<AccountDTO>();
                    string chargesString = resRules.Content.ReadAsStringAsync().Result;
                    float charges;
                    float.TryParse(chargesString, NumberStyles.Float, CultureInfo.InvariantCulture, out charges);
                    if (data.Status.ToString() != "RanToCompletion")
                    {
                        status = "There is no account with this Id";
                        return status;
                    }
                    else
                    {
                        account = data.Result;
                        account.Balance += amount;
                        account.Balance -= (int)charges;
                        var putTask = client.PutAsJsonAsync<AccountDTO>("Account/" + accountId, account);
                        putTask.Wait();
                        var result1 = putTask.Result;
                        if (result1.IsSuccessStatusCode)
                        {
                            transaction.Amount = amount;
                            transaction.AccountId = accountId;
                            transaction.Date = DateTime.Now;
                            transaction.TargerAccountId = accountId;
                            transaction.Type = "Deposit";
                            status = "Success";
                            transaction.TransactionStatus = status;
                            _context.Add(transaction);
                            _context.SaveChanges();
                        }
                    }
                }

            }
            catch (DbUpdateConcurrencyException Dbce)
            {
                status = Dbce.Message;
                Console.WriteLine(Dbce.Message);
            }
            catch (DbUpdateException Dbe)
            {
                status = Dbe.Message;
                Console.WriteLine(Dbe.Message);
            }
            return status;


        }


        public string Withdraw(int accountId, int amount)
        {
            string status = "";
            Transaction transaction = new Transaction();
            AccountDTO account = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41001/api/");
            var rules = new HttpClient();
            rules.BaseAddress = new Uri("http://localhost:41003/api/");
            try
            {
                var getTask = client.GetAsync("Account/" + accountId);
                getTask.Wait();
                var result = getTask.Result;

                var getRules = rules.GetAsync($"GetRules?balance={amount}&accountId={accountId}");
                getRules.Wait();
                var resRules = getRules.Result.Content.ReadAsStringAsync().Result;
                if (resRules!= "Allowed")
                {
                    status = resRules;
                    return status;
                }
                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadFromJsonAsync<AccountDTO>();
                    if (data.Status.ToString() != "RanToCompletion")
                    {
                        status = "There is no account with this Id";
                        return status;
                    }
                    else
                    {
                        account = data.Result;
                        account.Balance -= amount;
                        var putTask = client.PutAsJsonAsync<AccountDTO>("Account/" + accountId, account);
                        putTask.Wait();
                        var result1 = putTask.Result;
                        if (result1.IsSuccessStatusCode)
                        {
                            transaction.Amount = amount;
                            transaction.AccountId = accountId;
                            transaction.Date = DateTime.Now;
                            transaction.TargerAccountId = accountId;
                            transaction.Type = "Withdraw";
                            status = "Success";
                            transaction.TransactionStatus = status;
                            _context.Add(transaction);
                            _context.SaveChanges();
                        }
                    }
                }

            }
            catch (DbUpdateConcurrencyException Dbce)
            {
                status = Dbce.Message;
                Console.WriteLine(Dbce.Message);
            }
            catch (DbUpdateException Dbe)
            {
                status = Dbe.Message;
                Console.WriteLine(Dbe.Message);
            }
            return status;


        }

        public string Transfer(int sourceAccountId, int targerAccountId, int amount)
        {
            string status = "";
            Transaction transaction = new Transaction();
            AccountDTO sourceAccount = null;
            AccountDTO targerAccount = null;
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41001/api/");
            var rules = new HttpClient();
            rules.BaseAddress = new Uri("http://localhost:41003/api/");

            try
            {
                var getTaskFrom = client.GetAsync("Account/" + sourceAccountId);
                getTaskFrom.Wait();
                var resultFrom = getTaskFrom.Result;
                var getTaskTo = client.GetAsync("Account/" + targerAccountId);
                getTaskTo.Wait();
                var resultTo = getTaskTo.Result;
                
                var getRules = rules.GetAsync($"GetRules?balance={amount}&accountId={sourceAccountId}");
                getRules.Wait();
                var resRules = getRules.Result.Content.ReadAsStringAsync().Result;
                if (resRules != "Allowed")
                {
                    status = resRules;
                    return status;
                }
                if (resultFrom.IsSuccessStatusCode&&resultTo.IsSuccessStatusCode)
                {
                    var dataFrom = resultFrom.Content.ReadFromJsonAsync<AccountDTO>();
                    var dataTo = resultTo.Content.ReadFromJsonAsync<AccountDTO>();
                    if (dataFrom.Status.ToString() != "RanToCompletion" || dataTo.Status.ToString() != "RanToCompletion")
                    {
                        status = "There is no account with this Id";
                        return status;
                    }
                    else
                    {
                        sourceAccount = dataFrom.Result;
                        targerAccount = dataTo.Result;
                       
                        sourceAccount.Balance -= amount;
                        var putTaskFrom = client.PutAsJsonAsync<AccountDTO>("Account/" + sourceAccountId, sourceAccount);
                        putTaskFrom.Wait();
                        var result1 = putTaskFrom.Result;

                        targerAccount.Balance += amount;
                        var putTaskTo = client.PutAsJsonAsync<AccountDTO>("Account/" + targerAccountId, targerAccount);
                        putTaskTo.Wait();
                        var result2 = putTaskFrom.Result;
                        if (result2.IsSuccessStatusCode)
                        {
                            transaction.Amount = amount;
                            transaction.AccountId = sourceAccountId;
                            transaction.Date = DateTime.Now;
                            transaction.SourceAccountId = sourceAccountId;
                            transaction.TargerAccountId = targerAccountId;
                            transaction.Type = "Transfer";
                            status = "Success";
                            transaction.TransactionStatus = status;
                            _context.Add(transaction);
                            _context.SaveChanges();
                        }
                    }
                }

            }
            catch (DbUpdateConcurrencyException Dbce)
            {
                status = Dbce.Message;
                Console.WriteLine(Dbce.Message);
            }
            catch (DbUpdateException Dbe)
            {
                status = Dbe.Message;
                Console.WriteLine(Dbe.Message);
            }
            return status;


        }

    }
}

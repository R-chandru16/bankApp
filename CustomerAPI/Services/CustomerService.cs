using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CustomerAPI.Services
{
    public class CustomerService
    {
        private readonly CustomerContext _context;

        public CustomerService(CustomerContext context)
        {
            _context = context;
        }

        public CustomerCreationStatus Add(Customer customer)
        {
            CustomerCreationStatus customerCreationStatus = new CustomerCreationStatus();
            try
            {
                Customer customer1 = _context.customers.FirstOrDefault(p => p.Email == customer.Email);
                if (customer1 != null) 
                {
                    customerCreationStatus.CustomerEmail = customer.Email;
                    customerCreationStatus.Message = "Already exists";
                    _context.creationStatuses.Add(customerCreationStatus);
                    _context.SaveChanges();
                }
                else
                {
                    _context.customers.Add(customer);
                    customerCreationStatus.CustomerEmail = customer.Email;
                    customerCreationStatus.Message = "sucess";
                    _context.creationStatuses.Add(customerCreationStatus);
                    _context.SaveChanges();
                }
                return customerCreationStatus;
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

        public Customer Get(string email)
        {
            return _context.customers.FirstOrDefault(u => u.Email == email);
        }
    }
}

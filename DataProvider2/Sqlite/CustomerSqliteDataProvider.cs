﻿using Microsoft.EntityFrameworkCore;

// https://docs.microsoft.com/en-us/ef/core/providers/sqlite/functions

namespace WinUITest.Data
{
    public class CustomerSqliteDataProvider : ICustomerDataProvider
    {
        public SqliteContext DataContext { get; private set; }

        public CustomerSqliteDataProvider()
        {
            DataContext = new SqliteContext();
        }

        public IEnumerable<Customer> GetAll()
        {
            return DataContext.Customers;
        }

        public IEnumerable<Customer> SearchCustomers(string searchTerm)
        {
            //return DataContext.Customers
            //    .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
            //                || c.CustomerCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            //    .Take(10);
            return DataContext.Customers
                .Where(c => EF.Functions.Like(c.Name, $"%{searchTerm}%") || EF.Functions.Like(c.CustomerCode, $"%{searchTerm}%"))
                .Take(10);
        }

        public Customer? Get(int id)
        {
            return DataContext.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
        }

        public void Save(Customer c)
        {
            DataContext.Customers.Update(c);
            DataContext.SaveChanges();
        }

        public bool CustomerCodeExists(string customerCode)
        {
            return DataContext.Customers.Where(c => c.CustomerCode == customerCode).Count() > 0;
        }

        public bool CustomerHasTransactions(int customerId)
        {
            return DataContext.Transactions.Where(t => t.CustomerId == customerId).Count() > 0;
        }

        public void DeleteCustomer(int customerId)
        {
            var customerToDelete = Get(customerId);
            if (customerToDelete != null)
            {
                DataContext.Customers.Remove(customerToDelete);
                DataContext.SaveChanges();
            }
        }
    }
}

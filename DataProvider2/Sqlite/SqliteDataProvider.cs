using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class SqliteDataProvider : IDataProvider
    {
        public ICustomerDataProvider Customers => new CustomerSqliteDataProvider();
        public IProductDataProvider Products => new ProductSqliteDataProvider();
        public ITransactionDataProvider Transactions => new TransactionSqliteDataProvider();
    }
}


//public SqliteContext DataContext { get; private set; }

//public SqliteDataProvider()
//{
//    DataContext = new SqliteContext();
//}

//public IEnumerable<Customer> GetCustomers()
//{
//    return DataContext.Customers;
//}

//public IEnumerable<Customer> SearchCustomers(string searchTerm)
//{
//    //return DataContext.Customers
//    //    .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) 
//    //                || c.CustomerCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
//    //    .Take(10);
//    return DataContext.Customers
//        .Where(c => EF.Functions.Like(c.Name, $"%{searchTerm}%") || EF.Functions.Like(c.CustomerCode, $"%{searchTerm}%"))
//        .Take(10);
//}

//public Customer GetCustomer(int id)
//{
//    return DataContext.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
//}

//public void SaveCustomer(Customer c)
//{
//    DataContext.Customers.Update(c);
//    DataContext.SaveChanges();
//}
//    }
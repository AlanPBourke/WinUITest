using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WinUITest.Data
{
    public interface ICustomerDataProvider
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        void Save(Customer c);
        IEnumerable<Customer> SearchCustomers(string searchTerm);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

namespace WinUITest.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public bool IsCustomerSelected => SelectedCustomer != null;
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public ObservableCollection<CustomerViewModel> SearchResults = new();
        
        private CustomerViewModel _selectedCustomer;

        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    Debug.WriteLine($"{_selectedCustomer}");
                    RaisePropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
        }

        public void Load()
        {
            var customers = App.DataProvider.Customers.GetAll();
            
            Customers.Clear();

            foreach (var customer in customers)
            {
                Customers.Add(new CustomerViewModel(customer));
            }
        }

        public void SetCustomer(int customerId)
        {
            var customer = App.DataProvider.Customers.Get(customerId);
            SelectedCustomer = new CustomerViewModel(customer);

            Transactions.Clear();
            var transactionsForCustomer = App.DataProvider.Transactions.GetForCustomer(customerId);
            foreach (var transaction in transactionsForCustomer)
            {
                Transactions.Add(new TransactionViewModel(transaction));
            }
        }

        public void SearchCustomer(string searchTerm)
        {
            var results = App.DataProvider.Customers.SearchCustomers(searchTerm);
            SearchResults.Clear();
            foreach (var cust in results)
            {
                SearchResults.Add(new CustomerViewModel(cust));
            }
        }
    }
}

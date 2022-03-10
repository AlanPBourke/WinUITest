using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

namespace WinUITest.ViewModels
{
    public class CustomerMaintenanceViewModel : ViewModelBase
    {
        public bool IsCustomerSelected => SelectedCustomer != null;
        public bool IsTransactionSelected => SelectedCustomer != null;
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; } = new();
        //public ObservableCollection<CustomerViewModel> SearchResults = new();

        private CustomerViewModel _selectedCustomer;

        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TransactionViewModel _selectedTransaction;
        public TransactionViewModel SelectedTransaction
        {
            get => _selectedTransaction;
            set
            {
                if (_selectedTransaction != value)
                {
                    _selectedTransaction = value;
                    RaisePropertyChanged();
                }
            }
        }

        private TransactionDetailViewModel _selectedTransactionDetail;
        public TransactionDetailViewModel SelectedTransactionDetail
        {
            get => _selectedTransactionDetail;
            set
            {
                if (_selectedTransactionDetail != value)
                {
                    _selectedTransactionDetail = value;
                    RaisePropertyChanged();
                }
            }
        }
        public CustomerMaintenanceViewModel()
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

        public void SetTransaction(int transactionId)
        {
            var txn = App.DataProvider.Transactions.Get(transactionId);
            SelectedTransaction = new TransactionViewModel(txn);

            TransactionDetails.Clear();
            var transactionDetailsForTransaction = App.DataProvider.Transactions.GetTransactionDetailsForTransaction(SelectedTransaction.TransactionId);
            foreach (var transaction in transactionDetailsForTransaction)
            {
                TransactionDetails.Add(new TransactionDetailViewModel(transaction));
            }
        }

        private bool _isEditing { get; set; } = false;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                RaisePropertyChanged(nameof(IsEditing));
            }
        }

        private bool _isAdding { get; set; } = false;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                _isAdding = value;
                RaisePropertyChanged(nameof(IsAdding));
            }
        }

        public bool IsAddingOrEditing
        {
            get => _isAdding || _isEditing;
        }

        //public void SearchCustomer(string searchTerm)
        //{
        //    var results = App.DataProvider.Customers.SearchCustomers(searchTerm);
        //    SearchResults.Clear();
        //    foreach (var cust in results)
        //    {
        //        SearchResults.Add(new CustomerViewModel(cust));
        //    }
        //}
    }
}

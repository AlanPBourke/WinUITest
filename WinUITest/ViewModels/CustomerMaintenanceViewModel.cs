using System;
using System.Collections.ObjectModel;

namespace WinUITest.ViewModels
{
    public class CustomerMaintenanceViewModel : ViewModelBase
    {
        public bool IsTransactionSelected => SelectedCustomer != null;
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; } = new();
        //public ObservableCollection<CustomerViewModel> SearchResults = new();
        //public bool CustomerCodeError { get; set { RaisePropertyChanged(nameof(CustomerCodeError)); } }
        //public bool CustomerNameError { get; set; }

        private bool _isCustomerSelected;
        public bool IsCustomerSelected
        {
            get => _isCustomerSelected;
            set
            {
                _isCustomerSelected = SelectedCustomer != null;
                RaisePropertyChanged(nameof(IsCustomerSelected));
            }
        }

        private string _customerCodeErrorText = string.Empty;
        public string CustomerCodeErrorText
        {
            get => _customerCodeErrorText;
            set
            {
                _customerCodeErrorText = value;
                RaisePropertyChanged(nameof(CustomerCodeErrorText));
            }
        }

        private bool _customerCodeError;
        public bool CustomerCodeError
        {
            get => _customerCodeError;
            set
            {
                _customerCodeError = value;
                RaisePropertyChanged(nameof(CustomerCodeError));
            }
        }

        private bool _customerNameError;
        public bool CustomerNameError
        {
            get => _customerNameError;
            set
            {
                _customerNameError = value;
                RaisePropertyChanged(nameof(CustomerNameError));
            }
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged(nameof(SelectedCustomer));
                    IsCustomerSelected = true;
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
                    RaisePropertyChanged(nameof(SelectedTransaction));
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
                    RaisePropertyChanged(nameof(SelectedTransactionDetail));
                }
            }
        }
        public CustomerMaintenanceViewModel()
        {
        }

        public void SetFirstCustomer()
        {
            if (Customers.Count > 0)
            {
                SelectedCustomer = Customers[0];
            }
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

            if (customer != null)
            {
                SelectedCustomer = new CustomerViewModel(customer);
                //    RaisePropertyChanged(nameof(SelectedCustomer));
                //       RaisePropertyChanged(nameof(IsCustomerSelected));

                Transactions.Clear();
                var transactionsForCustomer = App.DataProvider.Transactions.GetForCustomer(customerId);
                foreach (var transaction in transactionsForCustomer)
                {
                    Transactions.Add(new TransactionViewModel(transaction));
                }
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
                RaisePropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isNavigating { get; set; } = true;
        public bool IsNavigating
        {
            get => _isNavigating;
            set
            {
                _isNavigating = value;
                RaisePropertyChanged(nameof(IsNavigating));
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
                RaisePropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isAddingOrEditing { get; } = false;
        public bool IsAddingOrEditing
        {
            get => IsAdding || IsEditing;
        }

        public bool Validate()
        {
            CustomerCodeError = false;
            CustomerNameError = false;

            if (String.IsNullOrWhiteSpace(SelectedCustomer.CustomerCode))
            {
                CustomerCodeError = true;
                CustomerCodeErrorText = "Customer code cannot be empty.";
            }
            else
            {
                if (IsAdding && App.DataProvider.Customers.CustomerCodeExists(SelectedCustomer.CustomerCode))
                {
                    CustomerCodeError = true;
                    CustomerCodeErrorText = "Customer code already exists.";
                }
            }

            if (String.IsNullOrWhiteSpace(SelectedCustomer.Name))
            {
                CustomerNameError = true;
            }

            return (CustomerCodeError == false && CustomerNameError == false);
        }

        public bool CanDelete()
        {
            CustomerCodeError = false;

            if (App.DataProvider.Customers.CustomerHasTransactions(SelectedCustomer.CustomerId))
            {
                CustomerCodeError = true;
                CustomerCodeErrorText = "Customer has transactions and cannot be deleted.";
                return false;
            }

            return true;
        }

        public void DeleteCustomer()
        {
            App.DataProvider.Customers.DeleteCustomer(SelectedCustomer.CustomerId);
        }


        //private bool _isAddingOrEditing { get; set; } = false;
        //public bool IsAddingOrEditing
        //{
        //    get => _isAddingOrEditing;
        //    set
        //    {
        //        _isAddingOrEditing = _isAdding || _isEditing;
        //        RaisePropertyChanged(nameof(IsAddingOrEditing));
        //    }

        //}

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

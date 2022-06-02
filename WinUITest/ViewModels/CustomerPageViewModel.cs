using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels
{
    public class CustomerPageViewModel : ObservableObject
    {
        //public bool IsTransactionSelected => SelectedCustomer != null;
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; } = new();

        private bool _isCustomerSelected;
        public bool IsCustomerSelected
        {
            get => _isCustomerSelected;
            set => SetProperty(ref _isCustomerSelected, value);
        }

        private CustomerViewModel _selectedCustomer;
        public CustomerViewModel SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                SetProperty(ref _selectedCustomer, value);
                IsCustomerSelected = true;
            }
        }

        private TransactionViewModel _selectedTransaction;
        public TransactionViewModel SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetProperty(ref _selectedTransaction, value);
        }

        private TransactionDetailViewModel _selectedTransactionDetail;
        public TransactionDetailViewModel SelectedTransactionDetail
        {
            get => _selectedTransactionDetail;
            set => SetProperty(ref _selectedTransactionDetail, value);
        }

        public CustomerPageViewModel()
        {
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                SetProperty(ref _isEditing, value);
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isNavigating;
        public bool IsNavigating
        {
            get => _isNavigating;
            set
            {
                SetProperty(ref _isNavigating, value);
            }
        }

        private bool _isAdding;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                SetProperty(ref _isAdding, value);
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        public bool IsAddingOrEditing
        {
            get => IsAdding || IsEditing;
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
                //OnPropertyChanged(nameof(SelectedCustomer));
                //OnPropertyChanged(nameof(IsCustomerSelected));

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
            var txn = App.DataProvider.Transactions.GetById(transactionId);
            SelectedTransaction = new TransactionViewModel(txn);

            TransactionDetails.Clear();
            var transactionDetailsForTransaction = App.DataProvider.Transactions.GetTransactionDetailsForId(SelectedTransaction.TransactionId);
            foreach (var transaction in transactionDetailsForTransaction)
            {
                TransactionDetails.Add(new TransactionDetailViewModel(transaction));
            }
        }


        public bool CanDelete()
        {
            return App.DataProvider.Customers.CustomerHasTransactions(SelectedCustomer.CustomerId) == false;
        }

        public void DeleteCustomer()
        {
            App.DataProvider.Customers.DeleteCustomer(SelectedCustomer.CustomerId);
        }
    }
}

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels
{
    public class CustomerMaintenanceViewModel : ObservableRecipient
    {
        public bool IsTransactionSelected => SelectedCustomer != null;
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; } = new();

        private bool _isCustomerSelected;
        public bool IsCustomerSelected
        {
            get => _isCustomerSelected;
            set
            {
                _isCustomerSelected = SelectedCustomer != null;
                OnPropertyChanged(nameof(IsCustomerSelected));
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
                    OnPropertyChanged(nameof(SelectedCustomer));
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
                    OnPropertyChanged(nameof(SelectedTransaction));
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
                    OnPropertyChanged(nameof(SelectedTransactionDetail));
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
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isNavigating { get; set; } = true;
        public bool IsNavigating
        {
            get => _isNavigating;
            set
            {
                _isNavigating = value;
                OnPropertyChanged(nameof(IsNavigating));
            }
        }

        private bool _isAdding { get; set; } = false;
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                _isAdding = value;
                OnPropertyChanged(nameof(IsAdding));
                OnPropertyChanged(nameof(IsAddingOrEditing));
            }
        }

        private bool _isAddingOrEditing { get; } = false;
        public bool IsAddingOrEditing
        {
            get => IsAdding || IsEditing;
        }



        public void DeleteCustomer()
        {
            App.DataProvider.Customers.DeleteCustomer(SelectedCustomer.CustomerId);
        }
    }
}

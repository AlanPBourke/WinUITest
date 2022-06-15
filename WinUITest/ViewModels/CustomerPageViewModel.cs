using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class CustomerPageViewModel : ObservableObject
{
    //public bool IsTransactionSelected => SelectedCustomer != null;
    public ObservableCollection<CustomerViewModel> Customers { get; } = new();
    public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
    public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; } = new();

    private IDataProvider DataProvider;

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

    // -- The constructor is never explicitly called with a parameter, instead the DI framework will
    // -- resolve it as long as there is a concrete instance of IDataProvider registered.
    // -- See App.xaml.cs
    public CustomerPageViewModel(IDataProvider provider)
    {
        //DataProvider = App.Current.Services.GetService(typeof(SqliteDataProvider)) as SqliteDataProvider;
        DataProvider = provider;
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
        var customers = DataProvider.Customers.GetAll();

        Customers.Clear();

        foreach (var customer in customers)
        {
            CustomerViewModel newcustmodel = App.Current.Services.GetService<CustomerViewModel>();
            newcustmodel.SetCustomer(customer);
            Customers.Add(newcustmodel);
        }
    }

    public void SetCustomer(int customerId)
    {
        var customer = DataProvider.Customers.Get(customerId);

        if (customer != null)
        {
            CustomerViewModel newcustmodel = App.Current.Services.GetService<CustomerViewModel>();
            newcustmodel.SetCustomer(customer);
            SelectedCustomer = newcustmodel;
            //OnPropertyChanged(nameof(SelectedCustomer));
            //OnPropertyChanged(nameof(IsCustomerSelected));

            Transactions.Clear();
            var transactionsForCustomer = DataProvider.Transactions.GetForCustomer(customerId);
            foreach (var transaction in transactionsForCustomer)
            {
                Transactions.Add(new TransactionViewModel(transaction));
            }
        }
    }

    public void SetTransaction(int transactionId)
    {
        var txn = DataProvider.Transactions.GetById(transactionId);
        SelectedTransaction = new TransactionViewModel(txn);

        TransactionDetails.Clear();
        var transactionDetailsForTransaction = DataProvider.Transactions.GetTransactionDetailsForId(SelectedTransaction.TransactionId);
        foreach (var transaction in transactionDetailsForTransaction)
        {
            TransactionDetails.Add(new TransactionDetailViewModel(transaction));
        }
    }


    public bool CanDelete()
    {
        return DataProvider.Customers.CustomerHasTransactions(SelectedCustomer.CustomerId) == false;
    }

    public void DeleteCustomer()
    {
        DataProvider.Customers.DeleteCustomer(SelectedCustomer.CustomerId);
    }
}

using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;
public class TransactionsPageViewModel : ObservableObject
{
    private IDataProvider DataProvider;
    public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
    public ObservableCollection<TransactionDetailViewModel> TransactionDetails { get; set; } = new();

    //public bool IsTransactionSelected => SelectedCustomer != null;

    private bool _isTransactionSelected;
    public bool IsTransactionSelected
    {
        get => _isTransactionSelected;
        set => SetProperty(ref _isTransactionSelected, value);
    }

    private TransactionViewModel _selectedTransaction;
    public TransactionViewModel SelectedTransaction
    {
        get => _selectedTransaction;
        set
        {
            SetProperty(ref _selectedTransaction, value);
            IsTransactionSelected = true;
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

    public bool IsAddingOrEditing
    {
        get => IsAdding || IsEditing;
    }

    public void SetTransaction(int transactionId)
    {
        var txn = DataProvider.Transactions.GetById(transactionId);

        if (txn != null)
        {
            TransactionViewModel newtxnviewmodel = App.Current.Services.GetService<TransactionViewModel>();
            newtxnviewmodel.SetTransaction(txn);
            SelectedTransaction = newtxnviewmodel;
            var detail = DataProvider.Transactions.GetTransactionDetailsForId(transactionId);
            TransactionDetails.Clear();
            foreach (var transactiondetail in detail)
            {
                var newtxndetail = App.Current.Services.GetService<TransactionDetailViewModel>();
                newtxndetail.SetTransactionDetail(transactiondetail);
                TransactionDetails.Add(newtxndetail);
            }
        }
    }

    public void SetTransaction2(TransactionViewModel txn)
    {
        if (txn != null)
        {
            SelectedTransaction = txn;
            OnPropertyChanged(nameof(SelectedTransaction));
        }
    }

    // TODO implement
    public bool CanDelete()
    {
        return false;
    }

    // TODO implement
    public void Delete()
    {

    }

    public void Load()
    {
        var transactions = DataProvider.Transactions.GetAll();

        Transactions.Clear();

        foreach (var transaction in transactions)
        {
            TransactionViewModel newtxnviewmodel = App.Current.Services.GetService<TransactionViewModel>();
            newtxnviewmodel.SetTransaction(transaction);
            Transactions.Add(newtxnviewmodel);
        }
    }


    public void SetFirstTransaction()
    {
        if (Transactions.Count > 0)
        {
            SelectedTransaction = Transactions[0];
        }
    }

    public TransactionsPageViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
    }
}

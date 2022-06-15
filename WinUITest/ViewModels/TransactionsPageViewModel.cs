using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels;
public class TransactionsPageViewModel : ObservableObject
{
    public List<TransactionViewModel> Transactions { get; } = new();
    //public bool IsTransactionSelected => SelectedCustomer != null;

    public void Load()
    {
        var transactions = App.DataProvider.Transactions.GetAll();

        Transactions.Clear();

        foreach (var transaction in transactions)
        {
            Transactions.Add(new TransactionViewModel(transaction));
        }
    }

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
        var txn = App.DataProvider.Transactions.GetById(transactionId);

        if (txn != null)
        {
            SelectedTransaction = new TransactionViewModel(txn);
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

    public void SetFirstTransaction()
    {
        if (Transactions.Count > 0)
        {
            SelectedTransaction = Transactions[0];
        }
    }

    public TransactionsPageViewModel()
    {

    }
}

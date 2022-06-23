// NOT USED

using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class EditTransactionViewModel : ObservableObject, IEditableObject
{
    private IDataProvider DataProvider;

    private Transaction _transaction;
    public Transaction Transaction
    {
        get => _transaction;
        set => SetProperty(ref _transaction, value);
    }

    private string _customercode;
    public string CustomerCode
    {
        get => _customercode;
        set => SetProperty(ref _customercode, value);
    }

    private DateTime _transactiondate;
    public DateTime TransactionDate
    {
        get => _transactiondate;
        set => SetProperty(ref _transactiondate, value);
    }

    private void SetTransaction(Transaction transaction)
    {
        _transaction = transaction;
        TransactionDate = transaction.TransactionDate;
        transaction.Customer
        _transaction.Customer = transaction.Customer;
        _transaction.CustomerId = transaction.CustomerId;
        _transaction.
    }

    //private bool _isTransactionDetailSelected;
    //public bool IsTransactionSelected
    //{
    //    get => _isTransactionDetailSelected;
    //    set => SetProperty(ref _isTransactionDetailSelected, value);
    //}

    //private TransactionDetailViewModel _selectedTransactionDetail;
    //public TransactionDetailViewModel SelectedTransactionDetail
    //{
    //    get => _selectedTransactionDetail;
    //    set
    //    {
    //        SetProperty(ref _selectedTransactionDetail, value);
    //        IsTransactionSelected = true;
    //    }
    //}

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


    //public void SetTransactionDetail(int transactionDetailId)
    //{
    //    var txndetail = DataProvider.Transaction.GetById(transactionId);

    //    if (txn != null)
    //    {
    //        TransactionViewModel newtxnviewmodel = App.Current.Services.GetService<TransactionViewModel>();
    //        newtxnviewmodel.SetTransaction(txn);
    //        SelectedTransaction = newtxnviewmodel;
    //    }
    //}

    // TODO implement
    public bool CanDelete()
    {
        return false;
    }


    public void Load()
    {
        Transaction = App.Current.Services.GetService<TransactionViewModel>();
        //TransactionDetailViewModel nd = App.Current.Services.GetService<TransactionDetailViewModel>();

        //nd.Quantity = 10;
        //nd.Value = 100;
        //nd.ProductCode = "PLIP";

        //NewTransactionDetails.Add(nd);

        //var newtransaction = App.Current.Services.GetService<TransactionViewModel>();
        //newtransaction.CustomerCode = "ABCD";
        //newtransaction.CustomerId = 99;
        //newtransaction.Type = "I";
        //newtransaction.Value = 12.99;
        //var newtransactiondetail = App.Current.Services.GetService<TransactionDetailViewModel>();
        //newtransactiondetail.ProductCode = "PLIP";
        //NewTransactionDetails.Add(newtransactiondetail);
    }


    //public void SetFirstTransaction()
    //{
    //    if (Transactions.Count > 0)
    //    {
    //        SelectedTransaction = Transactions[0];
    //    }
    //}

    public EditTransactionViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
    }

    public void Save()
    {
        Transaction.
        _product.ProductCode = ProductCode;
        _product.ProductName = ProductName;
        _product.Price = Price;
        DataProvider.Products.Save(_product);
    }

    public void Delete()
    {
        DataProvider.Products.Delete(_product.ProductId);
    }

    public void BeginEdit()
    {
        _backup = this.MemberwiseClone() as ProductViewModel;
    }

    public void CancelEdit()
    {
        ProductCode = _backup.ProductCode;
        ProductName = _backup.ProductName;
        Price = _backup.Price;
    }

    public void EndEdit()
    {

    }
}

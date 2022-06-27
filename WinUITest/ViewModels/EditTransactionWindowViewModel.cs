// NOT USED

using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class EditTransactionWindowViewModel : ObservableObject
{
    public List<string> CustomerList => new();

    private IDataProvider DataProvider;

    private TransactionViewModel _transaction;
    public TransactionViewModel Transaction
    {
        get => _transaction;
        set => SetProperty(ref _transaction, value);
    }

    private CustomerViewModel _selectedcustomer;
    public CustomerViewModel SelectedCustomer
    {
        get => _selectedcustomer;
        set => SetProperty(ref _selectedcustomer, value);
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


    public void SetTransaction(TransactionViewModel transactionViewModel)
    {
        _transaction = transactionViewModel;

    }

    // TODO implement
    public bool CanDelete()
    {
        return false;
    }


    public void Load()
    {
        Transaction = App.Current.Services.GetService<TransactionViewModel>();
        var custs = DataProvider.Customers.GetAll();
        foreach (Customer c in custs)
        {
            CustomerList.Add(c.Name);
        }

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

    public EditTransactionWindowViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
    }

    //public void Delete()
    //{
    //    DataProvider.Products.Delete(_product.ProductId);
    //}


}

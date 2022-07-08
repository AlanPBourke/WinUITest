// NOT USED

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class EditTransactionWindowViewModel : ObservableValidator, IEditableObject
{
    private IDataProvider DataProvider;

    //    public ObservableCollection<Customer> CustomerList { get; set; } = new ObservableCollection<Customer>();
    public ObservableCollection<CustomerViewModel> CustomerList { get; set; } = new();
    public ObservableCollection<ProductViewModel> ProductList { get; set; } = new();
    public ObservableCollection<TransactionDetailViewModel> TransactionDetailsList { get; set; } = new();

    private TransactionDetailViewModel _selectedtransactiondetail;
    public TransactionDetailViewModel SelectedTransactionDetail
    {
        get => _selectedtransactiondetail;
        set => SetProperty(ref _selectedtransactiondetail, value);
    }


    private Transaction _currenttransaction;
    public Transaction CurrentTransaction
    {
        get => _currenttransaction;
        set => SetProperty(ref _currenttransaction, value);
    }

    private Customer _selectedcustomer;
    public Customer SelectedCustomer
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


    public void SetTransaction(Transaction transaction)
    {
        _currenttransaction = transaction;
    }

    // TODO implement
    public bool CanDelete()
    {
        return false;
    }

    public void AddTransactionDetail()
    {
        //if (CurrentTransaction != null)
        //  {
        var newtxd = App.Current.Services.GetService(typeof(TransactionDetailViewModel)) as TransactionDetailViewModel;
        SelectedTransactionDetail = newtxd;
        IsAdding = true;
        IsEditing = false;
        IsNavigating = false;
        Debug.WriteLine($"Detail Lines:{TransactionDetailsList.Count}");
        //   }

        //TransactionDetailsList.Add(newtxdvm);
        //SelectedTransactionDetail = TransactionDetailsList[TransactionDetailsList.Count];
    }

    public void SaveTransactionDetail()
    {
        if (SelectedTransactionDetail != null)
        {
            TransactionDetailsList.Add(SelectedTransactionDetail);
            IsAdding = false;
            IsEditing = false;
            IsNavigating = true;
        }
    }

    public void AddTransactionDetail2()
    {
        TransactionDetail newtxd = new TransactionDetail();
        CurrentTransaction.TransactionDetails.Add(newtxd);
    }

    public void Load()
    {

        //CurrentTransaction = App.Current.Services.GetService<Transaction>();
        LoadCustomerList();
        LoadProductList();
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

    public void LoadCustomerList()
    {
        var custs = DataProvider.Customers.GetAll();
        CustomerList.Clear();
        foreach (Customer c in custs)
        {
            //CustomerList.Add(c);
            var n = App.Current.Services.GetService<CustomerViewModel>() as CustomerViewModel;
            n.SetCustomer(c);
            CustomerList.Add(n);
        }
        Debug.WriteLine($"ViewModel, Customer list loaded, {CustomerList.Count}");
    }

    public void LoadProductList()
    {
        var prods = DataProvider.Products.GetAll();
        ProductList.Clear();
        foreach (Product p in prods)
        {
            //CustomerList.Add(c);
            var n = App.Current.Services.GetService<ProductViewModel>() as ProductViewModel;
            n.SetProduct(p);
            ProductList.Add(n);
        }
        Debug.WriteLine($"ViewModel, Product list loaded, {ProductList.Count}");
    }

    public void BeginEdit()
    {
        throw new System.NotImplementedException();
    }

    public void CancelEdit()
    {
        throw new System.NotImplementedException();
    }

    public void EndEdit()
    {
        throw new System.NotImplementedException();
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
        CurrentTransaction = new Transaction();
    }

    //public void Delete()
    //{
    //    DataProvider.Products.Delete(_product.ProductId);
    //}


}

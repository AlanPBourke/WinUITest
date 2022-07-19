// NOT USED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class EditTransactionWindowViewModel : ObservableValidator, IEditableObject
{
    private IDataProvider DataProvider;
    public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);
    //    public ObservableCollection<Customer> CustomerList { get; set; } = new ObservableCollection<Customer>();
    public ObservableCollection<CustomerViewModel> CustomerList { get; set; } = new();
    public ObservableCollection<ProductViewModel> ProductList { get; set; } = new();
    public ObservableCollection<TransactionDetailViewModel> TransactionDetailsList { get; set; } = new();

    public String TransactionType { get; set; }


    private string _searchProductCode;
    public string SearchProductCode
    {
        get => _searchProductCode;
        set => SetProperty(ref _searchProductCode, value);
    }

    private TransactionDetailViewModel _selectedtransactiondetail;
    public TransactionDetailViewModel SelectedTransactionDetail
    {
        get => _selectedtransactiondetail;
        set => SetProperty(ref _selectedtransactiondetail, value);
    }

    private ProductViewModel _selectedproduct;
    public ProductViewModel SelectedProduct
    {
        get => _selectedproduct;
        set => SetProperty(ref _selectedproduct, value);
    }

    private Transaction _currenttransaction;
    public Transaction CurrentTransaction
    {
        get => _currenttransaction;
        set => SetProperty(ref _currenttransaction, value);
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

    public EditTransactionWindowViewModel()
    {
        PropertyChanged += EditTransactionWindowViewModel_PropertyChanged;
        ErrorsChanged += EditTransactionWindowViewModel_ErrorsChanged;
    }

    private void EditTransactionWindowViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
    }

    private void EditTransactionWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HasErrors))
        {
            OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
        }
    }

    public void SetTransaction(Transaction transaction)
    {
        _currenttransaction = transaction;
    }

    public void SetSelectedProduct(Product product)
    {
        ProductViewModel vm = new ProductViewModel(DataProvider);
        vm.SetProduct(product);
        SelectedProduct = vm;
        SelectedTransactionDetail.ProductCode = vm.ProductCode;
        ValidateAllProperties();
    }

    public void SetSelectedProduct2(Product product)
    {
        SelectedTransactionDetail.ProductCode = product.ProductCode;
        SelectedTransactionDetail.ProductName = product.ProductName;
        SelectedTransactionDetail.Price = product.Price;
        ValidateAllProperties();
    }

    public void SetSelectedCustomer(Customer customer)
    {
        CustomerViewModel vm = new CustomerViewModel(DataProvider);
        vm.SetCustomer(customer);
        SelectedCustomer = vm;
    }

    public void SetTransactionDetail(TransactionDetailViewModel detail)
    {
        SelectedTransactionDetail = detail;

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
        SelectedTransactionDetail.BeginEdit();
        //   }

        //TransactionDetailsList.Add(newtxdvm);
        //SelectedTransactionDetail = TransactionDetailsList[TransactionDetailsList.Count];
    }

    public void EditTransactionDetail(TransactionDetailViewModel editTransaction)
    {

        //ViewModel.SelectedTransactionDetail = txn;
        //ProductSearchBox.Text = txn.ProductCode;
        SearchProductCode = editTransaction.ProductCode;
        SelectedTransactionDetail = editTransaction;
        SelectedTransactionDetail.BeginEdit();
    }

    public void DeleteTransactionDetail()
    {
        if (SelectedTransactionDetail != null)
        {
            TransactionDetailsList.Remove(TransactionDetailsList
                .Where(d => d.TransactionDetailId == SelectedTransactionDetail.TransactionDetailId).Single());
        }
    }


    public void SaveTransactionDetail()
    {
        if (SelectedTransactionDetail != null && SelectedTransactionDetail.HasErrors == false)
        {
            // SelectedTransactionDetail.ProductCode = SelectedProduct.ProductCode;
            // SelectedTransactionDetail.ProductName = SelectedProduct.ProductName;
            SelectedTransactionDetail.Value = SelectedTransactionDetail.Price * SelectedTransactionDetail.Quantity;

            if (IsAdding)
            {
                TransactionDetailsList.Add(SelectedTransactionDetail);
            }

            var newtxd = App.Current.Services.GetService(typeof(TransactionDetailViewModel)) as TransactionDetailViewModel;
            SelectedTransactionDetail = newtxd;
            IsAdding = false;
            IsEditing = false;
            IsNavigating = true;
        }
    }

    public void Load()
    {

        //CurrentTransaction = App.Current.Services.GetService<Transaction>();
        LoadCustomerList();
        LoadProductList();
        IsNavigating = true;
        IsAdding = false;
        IsEditing = false;
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

    public List<Product> SearchProducts(string query)
    {
        IEnumerable<Product> results = DataProvider.Products.SearchProducts(query);
        return results.ToList();
    }

    public List<Customer> SearchCustomers(string query)
    {
        IEnumerable<Customer> results = DataProvider.Customers.SearchCustomers(query);
        return results.ToList();
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

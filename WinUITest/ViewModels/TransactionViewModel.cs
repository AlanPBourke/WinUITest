using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;
public class TransactionViewModel : ObservableValidator, IEditableObject
{
    private IDataProvider DataProvider;

    public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);
    private Transaction _transaction;
    private TransactionViewModel _backup;

    public string CustomerCode
    {
        get => _transaction.Customer.CustomerCode;
        set => _transaction.Customer.CustomerCode = value;
    }

    private int _transactionid;
    public int TransactionId
    {
        get => _transactionid;
        set => SetProperty(ref _transactionid, value, true);
    }

    private string _expandedtype;
    public string ExpandedType
    {
        get => _expandedtype;
        set => SetProperty(ref _expandedtype, value, true);
    }

    private string _type;
    public string Type
    {
        get => _type;
        set
        {
            SetProperty(ref _type, value, true);
            ExpandedType = value == "I" ? "Invoice" : "Credit";
        }
    }

    // SQLite doesn't have DateTime, it's stored as Text  
    // EF will translate it 
    public string TransactionDateAsString => _transaction.TransactionDate.ToShortDateString();

    private DateTime _transactiondate;
    public DateTime TransactionDate
    {
        get => _transactiondate;
        set => SetProperty(ref _transactiondate, value, true);
    }

    private int _customerid;
    public int CustomerId
    {
        get => _customerid;
        set => SetProperty(ref _customerid, value, true);
    }

    private string _valueasstring;
    public string ValueAsString
    {
        get => _valueasstring;
        set
        {
            _valueasstring = Value.ToString("N2");            // 2 dp
        }
    }

    private double _value;
    public double Value
    {
        get => _value;
        set => SetProperty(ref _value, value, true);
    }

    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value, true);
    }

    private Customer _customer;
    public Customer Customer
    {
        get => _customer;
        set => SetProperty(ref _customer, value, true);
    }

    private List<TransactionDetail> _transactiondetails;
    public List<TransactionDetail> TransactionDetails
    {
        get => _transactiondetails;
        set => SetProperty(ref _transactiondetails, value, true);
    }

    public void AddTransactionDetail()
    {
        TransactionDetails.Add(new TransactionDetail());
    }

    public TransactionViewModel(IDataProvider dataprovider)
    {
        DataProvider = dataprovider;
        PropertyChanged += TransactionViewModel_PropertyChanged;
        ErrorsChanged += TransactionViewModel_ErrorsChanged;

    }

    public void SetTransaction(Transaction transaction)
    {
        _transaction = transaction;
        TransactionId = transaction.TransactionId;
        CustomerId = transaction.CustomerId;
        Value = transaction.Value;
        Type = transaction.Type;
        Customer = transaction.Customer;
        _transactiondetails = transaction.TransactionDetails;
    }

    private void TransactionViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
    }

    private void TransactionViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HasErrors))
        {
            OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
        }
    }

    public void BeginEdit()
    {
        _backup = MemberwiseClone() as TransactionViewModel;
    }

    public void CancelEdit()
    {
        Value = _backup.Value;
        TransactionId = _backup.TransactionId;
        CustomerId = _backup.CustomerId;
        Value = _backup.Value;
        Type = _backup.Type;
        Customer = _backup.Customer;
        _transactiondetails = _backup.TransactionDetails;

    }

    public void EndEdit()
    {

    }

    // TODO implement
    public void Delete()
    {

    }
    public void Save()
    {
        DataProvider.Transactions.Save(_transaction);
    }
}

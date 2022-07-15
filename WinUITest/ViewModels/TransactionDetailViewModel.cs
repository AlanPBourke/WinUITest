using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class TransactionDetailViewModel : ObservableValidator, IEditableObject
{
    private TransactionDetail _transactionDetail;
    private TransactionDetailViewModel _backup;
    public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);
    public TransactionDetailViewModel()
    {
        PropertyChanged += TransactionDetailViewModel_PropertyChanged;
        ErrorsChanged += TransactionDetailViewModel_ErrorsChanged;
    }

    private string _productname;
    public string ProductName
    {
        get => _productname;
        set => SetProperty(ref _productname, value);
    }

    private string _productcode;
    public string ProductCode
    {
        get => _productcode;
        set => SetProperty(ref _productcode, value);
    }

    private string _quantitystring;
    public string QuantityString
    {
        get => _quantitystring;
        set => SetProperty(ref _quantitystring, value);
    }

    private int _quantity;

    // -- Note the final 'true' parameter in SetProperty is required for 
    // -- validation to work!
    [Required]
    [Range(1, 99, ErrorMessage = $"Quantity must be between 1 and 99.")]
    public int Quantity
    {
        get => _quantity;
        set
        {
            SetProperty(ref _quantity, value, true);
            QuantityString = value.ToString();
        }
    }

    private double _price;
    [Required]
    [Range(0.01, 999999999.99, ErrorMessage = $"Price must be between 0.01 and 999999999.99")]
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value, true);
    }

    private string _valuestring;
    public string ValueString
    {
        get => _valuestring;
        set => SetProperty(ref _valuestring, value);
    }

    private double _value;
    public double Value
    {
        get => _value;

        set
        {
            SetProperty(ref _value, value);
            ValueString = value.ToString();
        }
    }

    private int _productid;
    public int ProductId
    {
        get => _productid;
        set => SetProperty(ref _productid, value);
    }

    private int _transactiondetailid;
    public int TransactionDetailId
    {
        get => _transactiondetailid;
        set => SetProperty(ref _transactiondetailid, value);
    }

    private void TransactionDetailViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Errors)); // Update Errors on every Error change, so I can bind to it.
    }

    private void TransactionDetailViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HasErrors))
        {
            OnPropertyChanged(nameof(HasErrors)); // Update HasErrors on every change, so I can bind to it.
        }
    }

    public void SetTransactionDetail(TransactionDetail transactionDetail)
    {
        _transactionDetail = transactionDetail;
        TransactionDetailId = transactionDetail.TransactionDetailId;
        Quantity = transactionDetail.Quantity;
        Price = transactionDetail.Price;
        Value = transactionDetail.Value;
        ProductId = transactionDetail.ProductId;
        ProductName = transactionDetail.Product.ProductName;
        ProductCode = transactionDetail.Product.ProductCode;
    }

    public void BeginEdit()
    {
        _backup = this.MemberwiseClone() as TransactionDetailViewModel;
        ValidateAllProperties();
    }

    public void CancelEdit()
    {
        ProductCode = _backup.ProductCode;
        ProductName = _backup.ProductName;
        Price = _backup.Price;
        Quantity = _backup.Quantity;
        Value = _backup.Value;
    }

    public void EndEdit()
    {

    }

}

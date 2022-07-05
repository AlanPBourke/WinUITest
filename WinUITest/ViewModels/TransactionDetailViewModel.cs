using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class TransactionDetailViewModel : ObservableObject
{
    private TransactionDetail _transactionDetail;

    public TransactionDetailViewModel()
    {

    }

    public void SetTransactionDetail(TransactionDetail transactionDetail)
    {
        _transactionDetail = transactionDetail;
        TransactionDetailId = transactionDetail.TransactionDetailId;
        Quantity = transactionDetail.Quantity;
        Price = transactionDetail.Price;
        Value = transactionDetail.Value;
        ProductId = transactionDetail.ProductId;
    }

    private string _quantitystring;
    public string QuantityString
    {
        get => _quantitystring;
        set => SetProperty(ref _quantitystring, value);
    }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            SetProperty(ref _quantity, value);
            QuantityString = value.ToString();
        }
    }

    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
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


}

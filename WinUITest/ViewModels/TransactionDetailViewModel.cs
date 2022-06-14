using Microsoft.Toolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels;

public class TransactionDetailViewModel : ObservableObject
{
    private readonly TransactionDetail _transactionDetail;

    public TransactionDetailViewModel(TransactionDetail transactionDetail)
    {
        _transactionDetail = transactionDetail;
    }

    public string QuantityString
    {
        get => _transactionDetail.Quantity.ToString();
    }

    public int Quantity
    {
        get => _transactionDetail.Quantity;

        set
        {
            if (_transactionDetail.Quantity != value)
            {
                _transactionDetail.Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
    }

    public string ValueString
    {
        get => _transactionDetail.Value.ToString();
    }

    public double Value
    {
        get => _transactionDetail.Value;

        set
        {
            if (_transactionDetail.Value != value)
            {
                _transactionDetail.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
    public string ProductCode
    {
        get => _transactionDetail.ProductCode;

        set
        {
            if (_transactionDetail.ProductCode != value)
            {
                _transactionDetail.ProductCode = value;
                OnPropertyChanged(nameof(ProductCode));
            }
        }

    }

    public int TransactionId
    {
        get => _transactionDetail.TransactionId;
    }
}

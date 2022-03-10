using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

namespace WinUITest.ViewModels
{
    public class TransactionDetailViewModel : ViewModelBase
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
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }

        public string ValueString
        {
            get => _transactionDetail.Value.ToString();
        }

        public decimal Value
        {
            get => _transactionDetail.Value;

            set
            {
                if (_transactionDetail.Value != value)
                {
                    _transactionDetail.Value = value;
                    RaisePropertyChanged(nameof(Value));
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
                    RaisePropertyChanged(nameof(ProductCode));
                }
            }

        }

        public int TransactionId
        {
            get => _transactionDetail.TransactionId;
        }
    }
}

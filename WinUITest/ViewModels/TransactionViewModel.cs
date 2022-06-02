using CommunityToolkit.Mvvm.ComponentModel;
using WinUITest.Data;

namespace WinUITest.ViewModels
{
    public class TransactionViewModel : ObservableObject
    {
        private readonly Transaction _transaction;
        public string ExpandedType => _transaction.Type == "I" ? "Invoice" : "Credit";
        public string CustomerCode { get => _transaction.Customer.CustomerCode; }

        public int TransactionId
        {
            get => _transaction.TransactionId;
            set
            {
                if (_transaction.TransactionId != value)
                {
                    _transaction.TransactionId = value;
                    OnPropertyChanged(nameof(TransactionId));
                }

            }
        }

        public string Type
        {
            get => _transaction.Type;
            set
            {
                if (value != _transaction.Type)
                {
                    _transaction.Type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }

        public string TransactionDateAsString => _transaction.TransactionDate.ToShortDateString();

        public int CustomerId
        {
            get => _transaction.CustomerId;
            set
            {
                if (value != _transaction.CustomerId)
                {
                    _transaction.CustomerId = value;
                    OnPropertyChanged(nameof(CustomerId));
                }
            }
        }

        public string ValueAsString => Value.ToString("N2");            // 2 dp
        public double Value
        {
            get => _transaction.Value;
            set
            {
                if (value != _transaction.Value)
                {
                    _transaction.Value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public TransactionViewModel(Transaction transaction)
        {
            _transaction = transaction;
        }

        public void Save()
        {
            App.DataProvider.Transactions.Save(_transaction);
        }
    }
}

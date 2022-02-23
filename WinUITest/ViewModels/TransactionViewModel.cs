using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;
using WinUITest.ViewModel;

namespace WinUITest.ViewModel
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly Transaction _transaction;

        public int TransactionId 
        { 
            get => _transaction.TransactionId;
            set
            {
                if (_transaction.TransactionId != value)
                {
                    _transaction.TransactionId = value;
                    RaisePropertyChanged(nameof(TransactionId));
                }

            }
        }

        public string ExpandedType => _transaction.Type == "I" ? "Invoice" : "Credit";
        public string Type
        {
            get => _transaction.Type; 
            set
            {
                if (value != _transaction.Type)
                {
                    _transaction.Type = value;
                    RaisePropertyChanged(nameof(Type));
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
                    RaisePropertyChanged(nameof(CustomerId));
                }
            }
        }

        public string ValueAsString => Value.ToString("N2");            // 2 dp
        public Decimal Value
        {
            get => _transaction.Value;
            set
            { 
                if (value != _transaction.Value)
                {
                    _transaction.Value = value;
                    RaisePropertyChanged(nameof(Value));
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

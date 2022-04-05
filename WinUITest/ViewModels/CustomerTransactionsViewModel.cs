using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.ViewModels
{
    public class CustomerTransactionsViewModel : ViewModelBase
    {
        public ObservableCollection<TransactionViewModel> CustomerTransactions { get; } = new();
        public int CustomerId { get; set; }

        public void Load()
        {
            var txns = App.DataProvider.Transactions.GetForCustomer(CustomerId);

            CustomerTransactions.Clear();

            foreach (var txn in txns)
            {
                CustomerTransactions.Add(new TransactionViewModel(txn));
            }
        }
    }
}

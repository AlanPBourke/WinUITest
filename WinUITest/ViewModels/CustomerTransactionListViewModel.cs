using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WinUITest.ViewModels
{
    public class TransactionListViewModel : ObservableObject
    {
        public ObservableCollection<TransactionViewModel> Transactions { get; } = new();
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public void Load()
        {
            //var txns = App.DataProvider.Transactions.GetById

            //CustomerTransactions.Clear();

            //foreach (var txn in txns)
            //{
            //    CustomerTransactions.Add(new TransactionViewModel(txn));
            //}
        }
    }
}

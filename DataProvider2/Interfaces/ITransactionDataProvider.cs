using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public interface ITransactionDataProvider
    {
        IEnumerable<Transaction> GetForCustomer(int customerId);
        IEnumerable<TransactionDetail> GetTransactionDetailsForTransaction(int transactionId);
        Transaction Get(int id);
        void Save(Transaction p);
    }
}

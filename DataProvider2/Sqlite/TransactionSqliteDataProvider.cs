using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class TransactionSqliteDataProvider : ITransactionDataProvider
    {
        public SqliteContext DataContext { get; private set; }

        public TransactionSqliteDataProvider()
        {
            DataContext = new SqliteContext();
        }

        public IEnumerable<Transaction> GetForCustomer(int customerId)
        {
            return DataContext.Transactions.Where(t => t.CustomerId == customerId);
        }

        public Transaction Get(int id)
        {
            return DataContext.Transactions.Where(t => t.TransactionId == id).FirstOrDefault();
        }

        public void Save(Transaction p)
        {
            DataContext.Transactions.Update(p);
            DataContext.SaveChanges();
        }
    }
}

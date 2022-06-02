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

        public Transaction GetById(int id)
        {
            return DataContext.Transactions.Where(t => t.TransactionId == id).FirstOrDefault();
        }

        public void Save(Transaction p)
        {
            DataContext.Transactions.Update(p);
            DataContext.SaveChanges();
        }

        public IEnumerable<TransactionDetail> GetTransactionDetailsForId(int transactionId)
        {
            return DataContext.TransactionDetails.Where(td => td.TransactionId == transactionId);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return DataContext.Transactions;
        }

        //public List< GetTransactions()
        //{
        //    var query = from t in DataContext.Transactions
        //                join c in DataContext.Customers on t.CustomerId equals c.CustomerId
        //                orderby t.TransactionDate
        //                select new { c.CustomerCode, c.Name, t.TransactionDate, t.Type, t.Value };
        //    query.ToList<>
        //}
    }
}

namespace WinUITest.Data
{
    public interface ITransactionDataProvider
    {
        //IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetForCustomer(int customerId);
        IEnumerable<Transaction> GetAll();
        IEnumerable<TransactionDetail> GetTransactionDetailsForId(int transactionId);
        Transaction GetById(int id);
        void Save(Transaction p);
    }
}

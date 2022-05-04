namespace WinUITest.Data
{
    public interface ICustomerDataProvider
    {
        IEnumerable<Customer> GetAll();
        Customer? Get(int id);
        bool CustomerCodeExists(string customerCode);
        bool CustomerHasTransactions(int customerId);
        void DeleteCustomer(int customerId);
        void Save(Customer c);
        IEnumerable<Customer> SearchCustomers(string searchTerm);
    }
}

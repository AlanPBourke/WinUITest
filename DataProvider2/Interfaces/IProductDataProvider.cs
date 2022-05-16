namespace WinUITest.Data
{
    public interface IProductDataProvider
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        void Save(Product p);
        void Delete(int id);

        bool ProductInUse(int id);
    }
}

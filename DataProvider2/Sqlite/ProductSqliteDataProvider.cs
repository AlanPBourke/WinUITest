using Microsoft.EntityFrameworkCore;

namespace WinUITest.Data
{
    public class ProductSqliteDataProvider : IProductDataProvider
    {
        public SqliteContext DataContext { get; private set; }

        public ProductSqliteDataProvider()
        {
            DataContext = new SqliteContext();
        }
        public Product Get(int id)
        {
            return DataContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
        }

        public void Save(Product p)
        {
            DataContext.Products.Update(p);
            DataContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return DataContext.Products;
        }

        public void Delete(int id)
        {
            var product = DataContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                DataContext.Products.Remove(product);
                DataContext.SaveChanges();
            }
        }

        public bool ProductInUse(int id)
        {
            var product = DataContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
            if (product != null)
            {
                return DataContext.TransactionDetails.Where(p => p.ProductId == id).Any();
            }
            return false;
        }

        public IEnumerable<Product> SearchProducts(string searchTerm)
        {
            return DataContext.Products
                .Where(c => EF.Functions.Like(c.ProductCode, $"%{searchTerm}%") || EF.Functions.Like(c.ProductName, $"%{searchTerm}%"))
                .Take(20);
        }
    }
}

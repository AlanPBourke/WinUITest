using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUITest.Data;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public interface IProductDataProvider
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        void Save(Product p);
        void Delete(int id);
    }
}

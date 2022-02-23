using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public interface IDataProvider
    {
        ICustomerDataProvider Customers { get; }
        IProductDataProvider Products { get; }
        ITransactionDataProvider Transactions { get; }
        
    }
}

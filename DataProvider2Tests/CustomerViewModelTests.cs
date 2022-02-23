using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinUITest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data.Tests
{
    [TestClass()]
    public class CustomerViewModelTests
    {
        [TestInitialize()]
        public void Init()
        {
            
        }

        //[TestMethod()]
        //public void UpdateCustomer()
        //{
        //    CustomerVm = new CustomerViewModel(new CustomerSqliteDataProvider());
        //    CustomerVm.Load(1);
        //    var c = CustomerVm.GetCustomer();
        //    Console.WriteLine(c);
        //    var newname = System.DateTime.Now.ToString();
        //    c.Name = newname;
        //    CustomerVm.SaveCustomer();
        //    CustomerVm.Load(1);
        //    var c2 = CustomerVm.GetCustomer();
        //    Console.WriteLine(c2);
        //    Assert.AreEqual(newname, c2.Name);
        //}

        //[TestMethod()]
        //public void GetAllCustomers()
        //{
        //    var viewmodel = new CustomerViewModel(new CustomerSqliteDataProvider());
        //    var custs = viewmodel.Customers;
        //    foreach (var c in custs)
        //    {
        //        Console.WriteLine(c);
        //    }

        //    Assert.IsTrue(custs.Any());
        //}

        //[TestMethod()]
        //public void GetProducts()
        //{
        //    var p = Cxt.Products.Where(p => p.Nam ==)
        //    foreach (var product in p)
        //    {
        //        Console.WriteLine($"{product.Name}");
        //    }
        //    Assert.IsNotNull(p);
        //}

        //[TestMethod()]
        //public void UpdateProducts()
        //{
        //    var p = Cxt.GetProducts();
        //    var prod = p[0];
        //    var newname = $"{prod.ProductId.ToString()} {System.DateTime.Now.ToString()}";

        //    foreach (var product in p)
        //    {
        //        Console.WriteLine($"{product.Name}");
        //    }

        //    prod.Name = newname;

        //    Cxt.UpdateProduct(prod);

        //    p = Cxt.GetProducts();
        //    prod = p[0];

        //    Assert.AreEqual(prod.Name, newname);
        //}

    }
}
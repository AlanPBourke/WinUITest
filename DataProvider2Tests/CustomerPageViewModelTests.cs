using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinUITest.ViewModels;

namespace WinUITest.Data.Tests;

[TestClass()]
public class CustomerPageViewModelTests
{
    CustomerPageViewModel ModelUnderTest;
    SqliteDataProvider DataProvider;

    [TestInitialize()]
    public void Init()
    {
        DataProvider = new SqliteDataProvider();
        ModelUnderTest = new CustomerPageViewModel(DataProvider);

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

    [TestMethod()]
    public void GetAllCustomers()
    {
        ModelUnderTest.Load();
        var custs = ModelUnderTest.Customers;
        foreach (var c in custs)
        {
            Console.WriteLine(c);
        }

        Assert.IsTrue(custs.Count > 0);
    }

    [TestMethod()]
    public void GetTransactionsForCustomer()
    {
        ModelUnderTest.Load();
        var custs = ModelUnderTest.Customers;
        ModelUnderTest.SetCustomer(0);
        var trans = ModelUnderTest.Transactions;
        foreach (var t in trans)
        {
            Console.WriteLine(t.TransactionDate.ToString());
        }

        Assert.IsTrue(trans.Count > 0);
    }

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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application
// dotnet ef migrations add initial then dotnet ef database update

namespace WinUITest.Data
{
    public class SqliteContext : DbContext
    {
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={Path.Combine(Path.GetTempPath(), "winuitest.db")}");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductCode = "EGG48", ProductName = "48 Class A Eggs", Price = 12.99m },
                new Product { ProductId = 2, ProductCode = "MILK25L", ProductName = "25L Full Fat Milk", Price = 8.50m },
                new Product { ProductId = 3, ProductCode = "SUGAR2KG", ProductName = "2KG White Sugar", Price = 7.15m },
                new Product { ProductId = 4, ProductCode = "VAN001", ProductName = "500ml Vanilla Essence", Price = 22.19m }
                );

            modelBuilder.Entity<Customer>().HasMany(t => t.Transactions).WithOne(c => c.Customer).IsRequired();
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerCode = "A001", Name = "Acorn Antiques", Balance = 25.98m },
                new Customer { CustomerId = 2, CustomerCode = "M123", Name = "Milliways Restaurants Ltd", Balance = 0.00m },
                new Customer { CustomerId = 3, CustomerCode = "T014", Name = "Trotters Independent Traders", Balance = -25.00m },
                new Customer { CustomerId = 4, CustomerCode = "S001", Name = "Sunshine Desserts Ltd", Balance = 0.00m },
                new Customer { CustomerId = 5, CustomerCode = "P145", Name = "Bob's Burger Restaurants Ltd", Balance = 0.00m }
                );

            modelBuilder.Entity<Transaction>().Property(t => t.TransactionDate).HasDefaultValue(System.DateTime.Now);
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { TransactionId = 1, CustomerId = 1, TransactionDate = new DateTime(2021, 12, 13), Type = "I", Value = 48.17m},
                new Transaction { TransactionId = 2, CustomerId = 1, TransactionDate = new DateTime(2021, 12, 14), Type = "C", Value = -22.19m },
                new Transaction { TransactionId = 3, CustomerId = 3, TransactionDate = new DateTime(2021, 12, 14), Type = "C", Value = -14.30m }
                );

            modelBuilder.Entity<TransactionDetail>().HasOne(t => t.Transaction).WithMany(d => d.TransactionDetails).HasForeignKey(t => t.TransactionId);
            modelBuilder.Entity<TransactionDetail>().HasData(
                new TransactionDetail { TransactionDetailId = 1, TransactionId = 1, ProductCode = "EGG48", Quantity = 2, Value = 25.98m },
                new TransactionDetail { TransactionDetailId = 2, TransactionId = 1, ProductCode = "VAN001", Quantity = 1, Value = 22.19m },
                new TransactionDetail { TransactionDetailId = 3, TransactionId = 2, ProductCode = "VAN001", Quantity = 1, Value = -22.19m },
                new TransactionDetail { TransactionDetailId = 4, TransactionId = 3, ProductCode = "SUGAR2KG", Quantity = 2, Value = -14.30m }
                );

        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WinUITest.Data;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    [DbContext(typeof(SqliteContext))]
    [Migration("20220705162914_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.6");

            modelBuilder.Entity("WinUITest.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Balance = 25.98,
                            CustomerCode = "A001",
                            Name = "Acorn Antiques"
                        },
                        new
                        {
                            CustomerId = 2,
                            Balance = 0.0,
                            CustomerCode = "M123",
                            Name = "Milliways Restaurants Ltd"
                        },
                        new
                        {
                            CustomerId = 3,
                            Balance = -25.0,
                            CustomerCode = "T014",
                            Name = "Trotters Independent Traders"
                        },
                        new
                        {
                            CustomerId = 4,
                            Balance = 0.0,
                            CustomerCode = "S001",
                            Name = "Sunshine Desserts Ltd"
                        },
                        new
                        {
                            CustomerId = 5,
                            Balance = 0.0,
                            CustomerCode = "P145",
                            Name = "Bob's Burger Restaurants Ltd"
                        });
                });

            modelBuilder.Entity("WinUITest.Data.CustomerTransaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("TransactionId");

                    b.ToView("CustomerTransactions");
                });

            modelBuilder.Entity("WinUITest.Data.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransactionDetailId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Price = 12.99,
                            ProductCode = "EGG48",
                            ProductName = "48 Class A Eggs",
                            TransactionDetailId = 0
                        },
                        new
                        {
                            ProductId = 2,
                            Price = 8.5,
                            ProductCode = "MILK25L",
                            ProductName = "25L Full Fat Milk",
                            TransactionDetailId = 0
                        },
                        new
                        {
                            ProductId = 3,
                            Price = 7.1500000000000004,
                            ProductCode = "SUGAR2KG",
                            ProductName = "2KG White Sugar",
                            TransactionDetailId = 0
                        },
                        new
                        {
                            ProductId = 4,
                            Price = 22.190000000000001,
                            ProductCode = "VAN001",
                            ProductName = "500ml Vanilla Essence",
                            TransactionDetailId = 0
                        });
                });

            modelBuilder.Entity("WinUITest.Data.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue(new DateTime(2022, 7, 5, 17, 29, 14, 233, DateTimeKind.Local).AddTicks(7895));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("TransactionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            CustomerId = 1,
                            TransactionDate = new DateTime(2021, 12, 13, 0, 0, 0, 0, DateTimeKind.Local),
                            Type = "I",
                            Value = 48.170000000000002
                        },
                        new
                        {
                            TransactionId = 2,
                            CustomerId = 1,
                            TransactionDate = new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            Type = "C",
                            Value = -22.190000000000001
                        },
                        new
                        {
                            TransactionId = 3,
                            CustomerId = 3,
                            TransactionDate = new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local),
                            Type = "C",
                            Value = -14.300000000000001
                        });
                });

            modelBuilder.Entity("WinUITest.Data.TransactionDetail", b =>
                {
                    b.Property<int>("TransactionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("TransactionDetailId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetails");

                    b.HasData(
                        new
                        {
                            TransactionDetailId = 1,
                            Price = 12.99,
                            ProductId = 1,
                            Quantity = 2,
                            TransactionId = 1,
                            Value = 25.98
                        },
                        new
                        {
                            TransactionDetailId = 2,
                            Price = 22.190000000000001,
                            ProductId = 4,
                            Quantity = 1,
                            TransactionId = 1,
                            Value = 22.190000000000001
                        },
                        new
                        {
                            TransactionDetailId = 3,
                            Price = 22.190000000000001,
                            ProductId = 4,
                            Quantity = 1,
                            TransactionId = 2,
                            Value = -22.190000000000001
                        },
                        new
                        {
                            TransactionDetailId = 4,
                            Price = 7.1500000000000004,
                            ProductId = 3,
                            Quantity = 2,
                            TransactionId = 3,
                            Value = -14.300000000000001
                        });
                });

            modelBuilder.Entity("WinUITest.Data.Product", b =>
                {
                    b.HasOne("WinUITest.Data.TransactionDetail", null)
                        .WithOne("Product")
                        .HasForeignKey("WinUITest.Data.Product", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WinUITest.Data.Transaction", b =>
                {
                    b.HasOne("WinUITest.Data.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WinUITest.Data.TransactionDetail", b =>
                {
                    b.HasOne("WinUITest.Data.Transaction", "Transaction")
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("WinUITest.Data.Customer", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("WinUITest.Data.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });

            modelBuilder.Entity("WinUITest.Data.TransactionDetail", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
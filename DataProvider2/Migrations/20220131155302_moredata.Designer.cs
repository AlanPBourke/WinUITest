﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WinUITest.Data;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    [DbContext(typeof(SqliteContext))]
    [Migration("20220131155302_moredata")]
    partial class moredata
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("WinUITest.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

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
                            Balance = 40.00m,
                            CustomerCode = "A001",
                            Name = "Acorn Antiques"
                        },
                        new
                        {
                            CustomerId = 2,
                            Balance = 150.00m,
                            CustomerCode = "M123",
                            Name = "Milliways Restaurants Ltd"
                        },
                        new
                        {
                            CustomerId = 3,
                            Balance = -25.00m,
                            CustomerCode = "T014",
                            Name = "Trotters Independent Traders"
                        },
                        new
                        {
                            CustomerId = 4,
                            Balance = 0.00m,
                            CustomerCode = "S001",
                            Name = "Sunshine Desserts Ltd"
                        },
                        new
                        {
                            CustomerId = 5,
                            Balance = 253.00m,
                            CustomerCode = "P145",
                            Name = "Bob's Burger Restaurants Ltd"
                        });
                });

            modelBuilder.Entity("WinUITest.Data.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            TransactionId = 1,
                            CustomerId = 1,
                            Type = "I",
                            Value = 50.00m
                        },
                        new
                        {
                            TransactionId = 2,
                            CustomerId = 1,
                            Type = "C",
                            Value = -10.00m
                        },
                        new
                        {
                            TransactionId = 3,
                            CustomerId = 2,
                            Type = "I",
                            Value = 150.00m
                        },
                        new
                        {
                            TransactionId = 4,
                            CustomerId = 3,
                            Type = "C",
                            Value = -25.00m
                        },
                        new
                        {
                            TransactionId = 5,
                            CustomerId = 5,
                            Type = "C",
                            Value = -72.00m
                        },
                        new
                        {
                            TransactionId = 6,
                            CustomerId = 5,
                            Type = "I",
                            Value = 325.00m
                        });
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

            modelBuilder.Entity("WinUITest.Data.Customer", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerCode = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2022, 7, 5, 17, 29, 14, 233, DateTimeKind.Local).AddTicks(7895))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    TransactionDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.TransactionDetailId);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "TransactionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    TransactionDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_TransactionDetails_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TransactionDetails",
                        principalColumn: "TransactionDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 1, 25.98, "A001", "Acorn Antiques" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 2, 0.0, "M123", "Milliways Restaurants Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 3, -25.0, "T014", "Trotters Independent Traders" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 4, 0.0, "S001", "Sunshine Desserts Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 5, 0.0, "P145", "Bob's Burger Restaurants Ltd" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 1, 1, new DateTime(2021, 12, 13, 0, 0, 0, 0, DateTimeKind.Local), "I", 48.170000000000002 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 2, 1, new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "C", -22.190000000000001 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 3, 3, new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "C", -14.300000000000001 });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "Price", "ProductId", "Quantity", "TransactionId", "Value" },
                values: new object[] { 1, 12.99, 1, 2, 1, 25.98 });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "Price", "ProductId", "Quantity", "TransactionId", "Value" },
                values: new object[] { 2, 22.190000000000001, 4, 1, 1, 22.190000000000001 });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "Price", "ProductId", "Quantity", "TransactionId", "Value" },
                values: new object[] { 3, 22.190000000000001, 4, 1, 2, -22.190000000000001 });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "Price", "ProductId", "Quantity", "TransactionId", "Value" },
                values: new object[] { 4, 7.1500000000000004, 3, 2, 3, -14.300000000000001 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName", "TransactionDetailId" },
                values: new object[] { 1, 12.99, "EGG48", "48 Class A Eggs", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName", "TransactionDetailId" },
                values: new object[] { 2, 8.5, "MILK25L", "25L Full Fat Milk", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName", "TransactionDetailId" },
                values: new object[] { 3, 7.1500000000000004, "SUGAR2KG", "2KG White Sugar", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName", "TransactionDetailId" },
                values: new object[] { 4, 22.190000000000001, "VAN001", "500ml Vanilla Essence", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

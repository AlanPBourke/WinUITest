using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    public partial class transactiondetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 6);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 7, 17, 42, 23, 840, DateTimeKind.Local).AddTicks(8166));

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    TransactionDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: false)
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

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Balance",
                value: 25.98m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Balance",
                value: 0.00m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Balance",
                value: 0.00m);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName" },
                values: new object[] { 1, 12.99m, "EGG48", "48 Class A Eggs" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName" },
                values: new object[] { 2, 8.50m, "MILK25L", "25L Full Fat Milk" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName" },
                values: new object[] { 3, 7.15m, "SUGAR2KG", "2KG White Sugar" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Price", "ProductCode", "ProductName" },
                values: new object[] { 4, 22.19m, "VAN001", "500ml Vanilla Essence" });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "ProductCode", "Quantity", "TransactionId", "Value" },
                values: new object[] { 1, "EGG48", 2, 1, 25.98m });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "ProductCode", "Quantity", "TransactionId", "Value" },
                values: new object[] { 2, "VAN001", 1, 1, 22.19m });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "ProductCode", "Quantity", "TransactionId", "Value" },
                values: new object[] { 3, "VAN001", 1, 2, -22.19m });

            migrationBuilder.InsertData(
                table: "TransactionDetails",
                columns: new[] { "TransactionDetailId", "ProductCode", "Quantity", "TransactionId", "Value" },
                values: new object[] { 4, "SUGAR2KG", 2, 3, -14.30m });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1,
                columns: new[] { "TransactionDate", "Value" },
                values: new object[] { new DateTime(2021, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 48.17m });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 2,
                columns: new[] { "TransactionDate", "Value" },
                values: new object[] { new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), -22.19m });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 3,
                columns: new[] { "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 3, new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "C", -14.30m });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Transactions");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Balance",
                value: 40.00m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Balance",
                value: 150.00m);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Balance",
                value: 253.00m);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "Value",
                value: 50.00m);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 2,
                column: "Value",
                value: -10.00m);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 3,
                columns: new[] { "CustomerId", "Type", "Value" },
                values: new object[] { 2, "I", 150.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 4, 3, "C", -25.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 5, 5, "C", -72.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 6, 5, "I", 325.00m });
        }
    }
}

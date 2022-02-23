using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    public partial class moredata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 4, 0.00m, "S001", "Sunshine Desserts Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 5, 253.00m, "P145", "Bob's Burger Restaurants Ltd" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 5, 5, "C", -72.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 6, 5, "I", 325.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);
        }
    }
}

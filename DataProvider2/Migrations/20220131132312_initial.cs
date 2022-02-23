using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    public partial class initial : Migration
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
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
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
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 1, 40.00m, "A001", "Acorn Antiques" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 2, 150.00m, "M123", "Milliways Restaurants Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 3, -25.00m, "T014", "Trotters Independent Traders" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 1, 1, "I", 50.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 2, 1, "C", -10.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 3, 2, "I", 150.00m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "Type", "Value" },
                values: new object[] { 4, 3, "C", -25.00m });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}

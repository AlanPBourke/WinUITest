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
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductCode = table.Column<string>(type: "TEXT", nullable: true),
                    ProductName = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2022, 5, 13, 12, 2, 32, 967, DateTimeKind.Local).AddTicks(751))
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 1, 25.98m, "A001", "Acorn Antiques" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 2, 0.00m, "M123", "Milliways Restaurants Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 3, -25.00m, "T014", "Trotters Independent Traders" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 4, 0.00m, "S001", "Sunshine Desserts Ltd" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Balance", "CustomerCode", "Name" },
                values: new object[] { 5, 0.00m, "P145", "Bob's Burger Restaurants Ltd" });

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
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 1, 1, new DateTime(2021, 12, 13, 0, 0, 0, 0, DateTimeKind.Local), "I", 48.17m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 2, 1, new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "C", -22.19m });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionId", "CustomerId", "TransactionDate", "Type", "Value" },
                values: new object[] { 3, 3, new DateTime(2021, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), "C", -14.30m });

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

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            // Manually added to create view.
            migrationBuilder.Sql(@"
                CREATE VIEW CustomerTransactions as
                    select t.TransactionId, t.TransactionDate, t.Type, t.Value, c.CustomerCode, c.Name
                from Transactions t left join Customers c on t.CustomerId = c.CustomerId;");
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

            // Manually added to drop view.
            migrationBuilder.Sql(@"DROP VIEW CustomerTransactions;");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinUITest.DataProvider.Migrations
{
    public partial class maxlengths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 9, 15, 42, 35, 245, DateTimeKind.Local).AddTicks(5238),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2022, 2, 7, 17, 42, 23, 840, DateTimeKind.Local).AddTicks(8166));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TransactionDate",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2022, 2, 7, 17, 42, 23, 840, DateTimeKind.Local).AddTicks(8166),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2022, 2, 9, 15, 42, 35, 245, DateTimeKind.Local).AddTicks(5238));
        }
    }
}

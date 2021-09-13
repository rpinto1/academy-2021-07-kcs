using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class Add_UpdatedOn_to_DailyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "StockPrice",
                table: "DailyInfo",
                type: "decimal(10,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousClose",
                table: "DailyInfo",
                type: "decimal(10,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "DailyInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "DailyInfo");

            migrationBuilder.AlterColumn<decimal>(
                name: "StockPrice",
                table: "DailyInfo",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PreviousClose",
                table: "DailyInfo",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,4)",
                oldNullable: true);
        }
    }
}

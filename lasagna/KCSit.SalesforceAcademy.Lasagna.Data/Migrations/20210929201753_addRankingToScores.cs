using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class addRankingToScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ranking",
                table: "Scores",
                type: "int",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ranking",
                table: "Scores");

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

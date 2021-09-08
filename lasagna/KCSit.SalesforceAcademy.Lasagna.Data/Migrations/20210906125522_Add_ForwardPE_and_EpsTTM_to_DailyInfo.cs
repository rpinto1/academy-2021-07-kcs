using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class Add_ForwardPE_and_EpsTTM_to_DailyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EpsTTM",
                table: "DailyInfo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ForwardPe",
                table: "DailyInfo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpsTTM",
                table: "DailyInfo");

            migrationBuilder.DropColumn(
                name: "ForwardPe",
                table: "DailyInfo");
        }
    }
}

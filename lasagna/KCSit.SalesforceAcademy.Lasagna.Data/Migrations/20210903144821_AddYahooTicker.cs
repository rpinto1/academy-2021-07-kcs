using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class AddYahooTicker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YahooTicker",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YahooTicker",
                table: "Companies");
        }
    }
}

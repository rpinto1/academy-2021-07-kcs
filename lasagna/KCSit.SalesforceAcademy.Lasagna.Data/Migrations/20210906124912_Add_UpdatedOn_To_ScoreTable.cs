using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KCSit.SalesforceAcademy.Lasagna.Data.Migrations
{
    public partial class Add_UpdatedOn_To_ScoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Scores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Scores");
        }
    }
}

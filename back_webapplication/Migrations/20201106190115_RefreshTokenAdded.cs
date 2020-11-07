using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapplication.Migrations
{
    public partial class RefreshTokenAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "refreshtoken ",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "refreshtokenexpirytime",
                table: "User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refreshtoken ",
                table: "User");

            migrationBuilder.DropColumn(
                name: "refreshtokenexpirytime",
                table: "User");
        }
    }
}

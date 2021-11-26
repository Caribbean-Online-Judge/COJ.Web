using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class AccountsWithEmailConfirmedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Accounts");
        }
    }
}

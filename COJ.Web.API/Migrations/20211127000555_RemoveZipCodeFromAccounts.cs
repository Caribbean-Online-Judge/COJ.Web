using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class RemoveZipCodeFromAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Countries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

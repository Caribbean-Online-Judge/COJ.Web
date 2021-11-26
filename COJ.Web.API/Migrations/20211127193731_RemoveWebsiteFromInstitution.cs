using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class RemoveWebsiteFromInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Website",
                table: "Institutions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Institutions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

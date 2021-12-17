using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class ProblemsWithTotalTimeLimitField2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SizeLimit",
                table: "Problems",
                newName: "SourceCodeLengthLimit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceCodeLengthLimit",
                table: "Problems",
                newName: "SizeLimit");
        }
    }
}

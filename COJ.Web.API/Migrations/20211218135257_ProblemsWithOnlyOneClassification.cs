using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class ProblemsWithOnlyOneClassification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProblemClassifications_Problems_ProblemId",
                table: "ProblemClassifications");

            migrationBuilder.DropIndex(
                name: "IX_ProblemClassifications_ProblemId",
                table: "ProblemClassifications");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "ProblemClassifications");

            migrationBuilder.AddColumn<int>(
                name: "ClassificationsId",
                table: "Problems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_ClassificationsId",
                table: "Problems",
                column: "ClassificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationsId",
                table: "Problems",
                column: "ClassificationsId",
                principalTable: "ProblemClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationsId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_ClassificationsId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "ClassificationsId",
                table: "Problems");

            migrationBuilder.AddColumn<int>(
                name: "ProblemId",
                table: "ProblemClassifications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProblemClassifications_ProblemId",
                table: "ProblemClassifications",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProblemClassifications_Problems_ProblemId",
                table: "ProblemClassifications",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id");
        }
    }
}

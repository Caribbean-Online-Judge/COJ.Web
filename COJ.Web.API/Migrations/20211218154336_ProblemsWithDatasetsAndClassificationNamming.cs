using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class ProblemsWithDatasetsAndClassificationNamming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationsId",
                table: "Problems");

            migrationBuilder.RenameColumn(
                name: "ClassificationsId",
                table: "Problems",
                newName: "ClassificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Problems_ClassificationsId",
                table: "Problems",
                newName: "IX_Problems_ClassificationId");

            migrationBuilder.CreateTable(
                name: "ProblemDataSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Input = table.Column<string>(type: "text", nullable: false),
                    Output = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProblemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemDataSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemDataSets_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProblemDataSets_ProblemId",
                table: "ProblemDataSets",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationId",
                table: "Problems",
                column: "ClassificationId",
                principalTable: "ProblemClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationId",
                table: "Problems");

            migrationBuilder.DropTable(
                name: "ProblemDataSets");

            migrationBuilder.RenameColumn(
                name: "ClassificationId",
                table: "Problems",
                newName: "ClassificationsId");

            migrationBuilder.RenameIndex(
                name: "IX_Problems_ClassificationId",
                table: "Problems",
                newName: "IX_Problems_ClassificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_ProblemClassifications_ClassificationsId",
                table: "Problems",
                column: "ClassificationsId",
                principalTable: "ProblemClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

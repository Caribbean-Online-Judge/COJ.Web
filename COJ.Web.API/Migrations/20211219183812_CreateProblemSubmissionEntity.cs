using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class CreateProblemSubmissionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProblemSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    ProblemId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<double>(type: "double precision", nullable: false),
                    Memory = table.Column<long>(type: "bigint", nullable: false),
                    CpuTime = table.Column<int>(type: "integer", nullable: false),
                    TestCase = table.Column<int>(type: "integer", nullable: false),
                    MinTestCase = table.Column<int>(type: "integer", nullable: false),
                    MaxTestCase = table.Column<int>(type: "integer", nullable: false),
                    AverageCase = table.Column<int>(type: "integer", nullable: false),
                    Lock = table.Column<bool>(type: "boolean", nullable: false),
                    AcceptedTests = table.Column<int>(type: "integer", nullable: false),
                    JudgeFlag = table.Column<int>(type: "integer", nullable: false),
                    AcceptedCases = table.Column<int>(type: "integer", nullable: false),
                    FirstAccepted = table.Column<bool>(type: "boolean", nullable: false),
                    Accepted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemSubmissions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProblemSubmissions_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProblemSubmissions_Problems_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProblemSubmissions_AccountId",
                table: "ProblemSubmissions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemSubmissions_LanguageId",
                table: "ProblemSubmissions",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemSubmissions_ProblemId",
                table: "ProblemSubmissions",
                column: "ProblemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProblemSubmissions");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class CreateProblemSubmissionEntityRefactorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstAccepted",
                table: "ProblemSubmissions");

            migrationBuilder.RenameColumn(
                name: "JudgeFlag",
                table: "ProblemSubmissions",
                newName: "Verdict");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastJudgingDateTime",
                table: "ProblemSubmissions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceCode",
                table: "ProblemSubmissions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastJudgingDateTime",
                table: "ProblemSubmissions");

            migrationBuilder.DropColumn(
                name: "SourceCode",
                table: "ProblemSubmissions");

            migrationBuilder.RenameColumn(
                name: "Verdict",
                table: "ProblemSubmissions",
                newName: "JudgeFlag");

            migrationBuilder.AddColumn<bool>(
                name: "FirstAccepted",
                table: "ProblemSubmissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

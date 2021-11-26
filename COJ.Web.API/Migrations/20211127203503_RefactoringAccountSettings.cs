using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COJ.Web.API.Migrations
{
    public partial class RefactoringAccountSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrivateMessageNotifications",
                table: "AccountSettings");

            migrationBuilder.DropColumn(
                name: "ShowBirthday",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "WboardNotifications",
                table: "AccountSettings",
                newName: "ShowBirthday");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowBirthday",
                table: "AccountSettings",
                newName: "WboardNotifications");

            migrationBuilder.AddColumn<bool>(
                name: "NewPrivateMessageNotifications",
                table: "AccountSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowBirthday",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

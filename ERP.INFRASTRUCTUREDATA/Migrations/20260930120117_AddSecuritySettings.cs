using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.INFRASTRUCTUREDATA.Migrations
{
    /// <inheritdoc />
    public partial class AddSecuritySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForcePasswordChange",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LockoutDurationMinutes",
                table: "SystemSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumLoginAttempts",
                table: "SystemSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumPasswordLength",
                table: "SystemSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RequireNumber",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireSpecialCharacter",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequireUppercase",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SessionTimeoutMinutes",
                table: "SystemSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorAuthentication",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForcePasswordChange",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "LockoutDurationMinutes",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "MaximumLoginAttempts",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "MinimumPasswordLength",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "RequireNumber",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "RequireSpecialCharacter",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "RequireUppercase",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "SessionTimeoutMinutes",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "TwoFactorAuthentication",
                table: "SystemSettings");
        }
    }
}

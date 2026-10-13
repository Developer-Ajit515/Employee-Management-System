using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.INFRASTRUCTUREDATA.Migrations
{
    /// <inheritdoc />
    public partial class AddAppearanceSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CompactSidebar",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "DashboardLayout",
                table: "SystemSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SidebarCollapsed",
                table: "SystemSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "SystemSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompactSidebar",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "DashboardLayout",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "SidebarCollapsed",
                table: "SystemSettings");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "SystemSettings");
        }
    }
}

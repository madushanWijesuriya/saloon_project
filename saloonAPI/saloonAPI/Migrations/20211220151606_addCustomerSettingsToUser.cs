using Microsoft.EntityFrameworkCore.Migrations;

namespace saloonAPI.Migrations
{
    public partial class addCustomerSettingsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings");

            migrationBuilder.AddColumn<int>(
                name: "SettingId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings");

            migrationBuilder.DropColumn(
                name: "SettingId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings",
                column: "UserId");
        }
    }
}

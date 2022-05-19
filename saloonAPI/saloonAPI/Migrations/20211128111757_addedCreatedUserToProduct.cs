using Microsoft.EntityFrameworkCore.Migrations;

namespace saloonAPI.Migrations
{
    public partial class addedCreatedUserToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedUserId1",
                table: "Products",
                column: "CreatedUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreatedUserId1",
                table: "Products",
                column: "CreatedUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreatedUserId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedUserId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedUserId1",
                table: "Products");
        }
    }
}

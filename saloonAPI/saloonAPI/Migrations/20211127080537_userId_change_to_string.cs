using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace saloonAPI.Migrations
{
    public partial class userId_change_to_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_AspNetUsers_UserId1",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_AspNetUsers_CreatedUserId1",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSettings_AspNetUsers_UserId1",
                table: "CustomerSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId1",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId1",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QRs_AspNetUsers_UserId1",
                table: "QRs");

            migrationBuilder.DropForeignKey(
                name: "FK_TelNumbers_AspNetUsers_UserId1",
                table: "TelNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCoupons_AspNetUsers_UserId1",
                table: "UserCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_AspNetUsers_UserId1",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserProducts_UserId1",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserCoupons_UserId1",
                table: "UserCoupons");

            migrationBuilder.DropIndex(
                name: "IX_TelNumbers_UserId1",
                table: "TelNumbers");

            migrationBuilder.DropIndex(
                name: "IX_QRs_UserId1",
                table: "QRs");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId1",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSettings_UserId1",
                table: "CustomerSettings");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CreatedUserId1",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_UserId1",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserProducts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCoupons");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TelNumbers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "QRs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ProductComments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CustomerSettings");

            migrationBuilder.DropColumn(
                name: "CreatedUserId1",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Appoinments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProducts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserCoupons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TelNumbers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TelNumbers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "QRs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CustomerSettings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Appoinments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_UserId",
                table: "UserProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoupons_UserId",
                table: "UserCoupons",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TelNumbers_UserId",
                table: "TelNumbers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QRs_UserId",
                table: "QRs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CreatedUserId",
                table: "Coupons",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_UserId",
                table: "Appoinments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_AspNetUsers_UserId",
                table: "Appoinments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_AspNetUsers_CreatedUserId",
                table: "Coupons",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSettings_AspNetUsers_UserId",
                table: "CustomerSettings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QRs_AspNetUsers_UserId",
                table: "QRs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelNumbers_AspNetUsers_UserId",
                table: "TelNumbers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCoupons_AspNetUsers_UserId",
                table: "UserCoupons",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_AspNetUsers_UserId",
                table: "UserProducts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appoinments_AspNetUsers_UserId",
                table: "Appoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_AspNetUsers_CreatedUserId",
                table: "Coupons");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSettings_AspNetUsers_UserId",
                table: "CustomerSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId",
                table: "ProductComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QRs_AspNetUsers_UserId",
                table: "QRs");

            migrationBuilder.DropForeignKey(
                name: "FK_TelNumbers_AspNetUsers_UserId",
                table: "TelNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCoupons_AspNetUsers_UserId",
                table: "UserCoupons");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProducts_AspNetUsers_UserId",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserProducts_UserId",
                table: "UserProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserCoupons_UserId",
                table: "UserCoupons");

            migrationBuilder.DropIndex(
                name: "IX_TelNumbers_UserId",
                table: "TelNumbers");

            migrationBuilder.DropIndex(
                name: "IX_QRs_UserId",
                table: "QRs");

            migrationBuilder.DropIndex(
                name: "IX_ProductComments_UserId",
                table: "ProductComments");

            migrationBuilder.DropIndex(
                name: "IX_Likes_UserId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSettings_UserId",
                table: "CustomerSettings");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CreatedUserId",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Appoinments_UserId",
                table: "Appoinments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TelNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserProducts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCoupons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserCoupons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TelNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TelNumbers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "QRs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "QRs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ProductComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Likes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CustomerSettings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CustomerSettings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedUserId",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId1",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Appoinments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Appoinments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProducts_UserId1",
                table: "UserProducts",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCoupons_UserId1",
                table: "UserCoupons",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TelNumbers_UserId1",
                table: "TelNumbers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_QRs_UserId1",
                table: "QRs",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComments_UserId1",
                table: "ProductComments",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId1",
                table: "Likes",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSettings_UserId1",
                table: "CustomerSettings",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CreatedUserId1",
                table: "Coupons",
                column: "CreatedUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appoinments_UserId1",
                table: "Appoinments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Appoinments_AspNetUsers_UserId1",
                table: "Appoinments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_AspNetUsers_CreatedUserId1",
                table: "Coupons",
                column: "CreatedUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSettings_AspNetUsers_UserId1",
                table: "CustomerSettings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId1",
                table: "Likes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_UserId1",
                table: "ProductComments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QRs_AspNetUsers_UserId1",
                table: "QRs",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TelNumbers_AspNetUsers_UserId1",
                table: "TelNumbers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCoupons_AspNetUsers_UserId1",
                table: "UserCoupons",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProducts_AspNetUsers_UserId1",
                table: "UserProducts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

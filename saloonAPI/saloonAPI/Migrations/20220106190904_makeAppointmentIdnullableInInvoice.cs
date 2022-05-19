using Microsoft.EntityFrameworkCore.Migrations;

namespace saloonAPI.Migrations
{
    public partial class makeAppointmentIdnullableInInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Appoinments_AppoinmentId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "AppoinmentId",
                table: "Invoices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Appoinments_AppoinmentId",
                table: "Invoices",
                column: "AppoinmentId",
                principalTable: "Appoinments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Appoinments_AppoinmentId",
                table: "Invoices");

            migrationBuilder.AlterColumn<int>(
                name: "AppoinmentId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Appoinments_AppoinmentId",
                table: "Invoices",
                column: "AppoinmentId",
                principalTable: "Appoinments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

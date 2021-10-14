using Microsoft.EntityFrameworkCore.Migrations;

namespace Odontology.Persistance.Migrations
{
    public partial class ChangedEmployeePropertyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Employee_DoctorId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Visits");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_EmployeeId",
                table: "Visits",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Employee_EmployeeId",
                table: "Visits",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Employee_EmployeeId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_EmployeeId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Visits");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Visits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Visits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_DoctorId",
                table: "Visits",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_PatientId",
                table: "Visits",
                column: "PatientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Employee_DoctorId",
                table: "Visits",
                column: "DoctorId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

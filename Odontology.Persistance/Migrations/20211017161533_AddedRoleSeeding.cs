using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odontology.Persistance.Migrations
{
    public partial class AddedRoleSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Name", "NormalizedName", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, "ea68a124-223b-463d-8c20-1fcd37f9d0ea", "System", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(7730), "Admin", null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Name", "NormalizedName", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 2, "2bb76cc9-13df-4cc4-84e7-af988b5d6238", "System", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(8743), "Doctor", null, null, null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedOn", "Name", "NormalizedName", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 3, "59c37100-fae7-44fc-ba7f-6897a7ab9bcb", "System", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(8831), "User", null, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

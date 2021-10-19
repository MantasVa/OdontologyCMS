using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odontology.Persistance.Migrations
{
    public partial class AddedRoleNormalizedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "a971b5de-b94a-4a60-a864-a1413afb5049", new DateTime(2021, 10, 18, 12, 58, 18, 404, DateTimeKind.Utc).AddTicks(2243), "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "5605eabf-7abe-4097-86d8-3861f9cc771f", new DateTime(2021, 10, 18, 12, 58, 18, 404, DateTimeKind.Utc).AddTicks(2733), "DOCTOR" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "494a62bc-0072-43ec-8119-6058a4fcdd2e", new DateTime(2021, 10, 18, 12, 58, 18, 404, DateTimeKind.Utc).AddTicks(2745), "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "ea68a124-223b-463d-8c20-1fcd37f9d0ea", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(7730), null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "2bb76cc9-13df-4cc4-84e7-af988b5d6238", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(8743), null });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "NormalizedName" },
                values: new object[] { "59c37100-fae7-44fc-ba7f-6897a7ab9bcb", new DateTime(2021, 10, 17, 16, 15, 32, 618, DateTimeKind.Utc).AddTicks(8831), null });
        }
    }
}

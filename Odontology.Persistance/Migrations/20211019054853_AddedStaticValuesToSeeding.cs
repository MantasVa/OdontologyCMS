using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odontology.Persistance.Migrations
{
    public partial class AddedStaticValuesToSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "862e4b44-03a4-4914-8a58-7f5349d4a893", new DateTime(2021, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "0a5b3069-55ae-449e-bc45-f4c47b86bfd1", new DateTime(2021, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "50ba9d13-30ef-4227-96ac-71b5352b2e52", new DateTime(2021, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "c0cda6c6-c09a-42fc-b813-2a90c2007eba", new DateTime(2021, 10, 19, 5, 31, 48, 528, DateTimeKind.Utc).AddTicks(217) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "da7a58fb-9aa7-4d3a-85f1-c39bfbe12258", new DateTime(2021, 10, 19, 5, 31, 48, 528, DateTimeKind.Utc).AddTicks(1083) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "36b742b0-241e-458a-88e7-ba2742e039c7", new DateTime(2021, 10, 19, 5, 31, 48, 528, DateTimeKind.Utc).AddTicks(1093) });
        }
    }
}

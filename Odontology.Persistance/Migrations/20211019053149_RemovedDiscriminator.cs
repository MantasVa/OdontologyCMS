using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Odontology.Persistance.Migrations
{
    public partial class RemovedDiscriminator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "4ad677ef-0170-44ac-84f0-65e4c58d9857", new DateTime(2021, 10, 19, 5, 28, 39, 808, DateTimeKind.Utc).AddTicks(3924) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "421644c3-55ed-4001-8d00-0c30c7a4d72d", new DateTime(2021, 10, 19, 5, 28, 39, 808, DateTimeKind.Utc).AddTicks(4524) });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedOn" },
                values: new object[] { "8a9b3166-a8b2-4bfb-a82c-179cfb8349fd", new DateTime(2021, 10, 19, 5, 28, 39, 808, DateTimeKind.Utc).AddTicks(4536) });
        }
    }
}

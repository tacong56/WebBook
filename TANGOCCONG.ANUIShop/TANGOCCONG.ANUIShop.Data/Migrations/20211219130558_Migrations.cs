using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 454, DateTimeKind.Local).AddTicks(7330),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 729, DateTimeKind.Local).AddTicks(1263));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 427, DateTimeKind.Local).AddTicks(3365),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 703, DateTimeKind.Local).AddTicks(6958));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 423, DateTimeKind.Local).AddTicks(7778),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 700, DateTimeKind.Local).AddTicks(3889));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "26f1d760-7632-4491-9c11-3b3a166d64e4");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "bf473bf2-b687-441c-b7e1-a2ba18ee01be");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "cf220701-9b6e-4cd4-9f6f-398561fa9804");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "b2fbc2c8-a1fa-41a4-b78f-65ad4ce19395", "AQAAAAEAACcQAAAAEDPg3NOH6g626RDdlLxJqbpLqDHLqSZI2mMKc7e2Tnrhvl0z0bd+KMM5GZsTvzP+vQ==", new DateTime(2021, 12, 19, 20, 5, 57, 486, DateTimeKind.Local).AddTicks(6130) });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 729, DateTimeKind.Local).AddTicks(1263),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 454, DateTimeKind.Local).AddTicks(7330));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 703, DateTimeKind.Local).AddTicks(6958),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 427, DateTimeKind.Local).AddTicks(3365));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 18, 45, 37, 700, DateTimeKind.Local).AddTicks(3889),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 20, 5, 57, 423, DateTimeKind.Local).AddTicks(7778));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d351c402-ac8e-43d5-a864-146d58462b49");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "867ffa76-408f-4026-81e3-38fb92bd6f50");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d0d07571-665d-4bd1-bdf2-6df4736157b9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "0367ce23-9fa1-4665-99e6-f74dedc1067e", "AQAAAAEAACcQAAAAELG62xagPU48a2HxN+4MPzZDyjRZLQAa5vkHmzTW4pMhIYDT3Eo8gPFTJtPeCSxyNQ==", new DateTime(2021, 12, 19, 18, 45, 37, 761, DateTimeKind.Local).AddTicks(1354) });
        }
    }
}

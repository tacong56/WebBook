using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class updatedb19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 0, 14, 40, 28, DateTimeKind.Local).AddTicks(2281),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 329, DateTimeKind.Local).AddTicks(425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 0, 14, 39, 951, DateTimeKind.Local).AddTicks(6075),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 298, DateTimeKind.Local).AddTicks(4615));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 0, 14, 39, 944, DateTimeKind.Local).AddTicks(7438),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 295, DateTimeKind.Local).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "80b748dc-2aad-4766-b1aa-eadc4ee96c79");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "56a83349-aed9-45e9-a872-1c1f9e84f12e");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b994bfdb-f37e-475a-9eec-95372bc8c383");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "8e36bcf9-6f75-4cf6-9087-1c84e4ff9a3b", "AQAAAAEAACcQAAAAEDufrKXwLWCR+V6IplYB9FgNkLBjPr52blsdlf76drAA4lO6D9SOWy5A9N5hLrS9aA==", new DateTime(2021, 12, 28, 0, 14, 40, 112, DateTimeKind.Local).AddTicks(9177) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 329, DateTimeKind.Local).AddTicks(425),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 0, 14, 40, 28, DateTimeKind.Local).AddTicks(2281));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 298, DateTimeKind.Local).AddTicks(4615),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 0, 14, 39, 951, DateTimeKind.Local).AddTicks(6075));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 295, DateTimeKind.Local).AddTicks(587),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 0, 14, 39, 944, DateTimeKind.Local).AddTicks(7438));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b3108a67-5098-40b8-b903-5341157ace6c");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "79f5fe18-6630-42a2-8f96-1c2fec4eca8a");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "d00803d5-54b1-4f1f-b459-21ea47d87d74");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "98dd9623-fa89-4583-a098-59df2f521b42", "AQAAAAEAACcQAAAAEDAddgDqD5DDHS4VYN/sWTOUovn/eMgBKWUSXKmAxLhXnExresCoxRO/x6SJGfLCEQ==", new DateTime(2021, 12, 27, 23, 42, 1, 363, DateTimeKind.Local).AddTicks(3489) });
        }
    }
}

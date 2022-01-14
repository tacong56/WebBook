using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class seeding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 131, DateTimeKind.Local).AddTicks(2001),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 752, DateTimeKind.Local).AddTicks(6604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 56, DateTimeKind.Local).AddTicks(10),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 696, DateTimeKind.Local).AddTicks(8843));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 49, DateTimeKind.Local).AddTicks(2384),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 690, DateTimeKind.Local).AddTicks(8815));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "d154c070-8565-41b1-b268-34acdf4384e1", "Admintrator role", "Admin", "admin" },
                    { 2, "2ec2377e-61a6-4336-af27-a46d9d6e4bfc", "Employee role", "Nhân viên", "employee" },
                    { 3, "8e9945a3-c9a9-4ce5-a2be-5d7c4abd146c", "Customer role", "Khách hàng", "customer" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "ImageId", "IsDelete", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "State", "TimeCreated", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, null, "b928cda7-147c-4a13-a26c-7e861c214854", new DateTime(1997, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "tacong56@gmail.com", true, "Ta", 0, false, "Cong", false, null, "tacong56@gmail.com", "admin", "AQAAAAEAACcQAAAAEK1Lp1xHZEWe+GRqcV9jqYIrAgNHTANI+afbBfHZ9xDUm9xIQfW9GDjXtyDdsHpiOg==", null, false, "", 0, new DateTime(2022, 1, 2, 16, 31, 54, 229, DateTimeKind.Local).AddTicks(3424), false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 752, DateTimeKind.Local).AddTicks(6604),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 131, DateTimeKind.Local).AddTicks(2001));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 696, DateTimeKind.Local).AddTicks(8843),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 56, DateTimeKind.Local).AddTicks(10));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2022, 1, 2, 16, 29, 56, 690, DateTimeKind.Local).AddTicks(8815),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2022, 1, 2, 16, 31, 54, 49, DateTimeKind.Local).AddTicks(2384));
        }
    }
}

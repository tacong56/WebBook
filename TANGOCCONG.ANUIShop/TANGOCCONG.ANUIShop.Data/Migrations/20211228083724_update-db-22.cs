using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class updatedb22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 132, DateTimeKind.Local).AddTicks(4882),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 380, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 67, DateTimeKind.Local).AddTicks(556),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 296, DateTimeKind.Local).AddTicks(161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 59, DateTimeKind.Local).AddTicks(9958),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 288, DateTimeKind.Local).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5768bfd3-3951-48de-8cdd-88090e6c2128");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b5d6d6bb-ffd9-4d39-8040-37165b222451");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ad2d331a-4535-453c-82b9-01e92b0c684a");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "e9e64511-85cc-4d45-95ec-bcfa66174d0a", "AQAAAAEAACcQAAAAEFWYMCfS82wKN4TWiStxmdG1NT2H0v4v5tKrB1TA9dPhgC5LwruvRmFZ4CQcdYq8Yg==", new DateTime(2021, 12, 28, 15, 37, 22, 206, DateTimeKind.Local).AddTicks(455) });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 380, DateTimeKind.Local).AddTicks(484),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 132, DateTimeKind.Local).AddTicks(4882));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 296, DateTimeKind.Local).AddTicks(161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 67, DateTimeKind.Local).AddTicks(556));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 288, DateTimeKind.Local).AddTicks(6894),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 15, 37, 22, 59, DateTimeKind.Local).AddTicks(9958));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6cf5b069-aa86-4f5f-91b7-9014158a70fd");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "47bab5db-f03a-4371-9783-563b701e6fb8");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "58ec8e7c-b66b-42c2-9021-bad0a52fd1c4");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "16116e9b-5f97-4cde-84e8-ef243096d1ee", "AQAAAAEAACcQAAAAEAiP+71yv7T9gyclMsEZQ3xHA+VvVMcr4EXmjpTZ46T7mkh7brlfj98N+6qFsv/UXw==", new DateTime(2021, 12, 28, 13, 29, 55, 466, DateTimeKind.Local).AddTicks(7693) });
        }
    }
}

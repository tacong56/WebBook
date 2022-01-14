using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class updatedb21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 380, DateTimeKind.Local).AddTicks(484),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 172, DateTimeKind.Local).AddTicks(4071));

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 296, DateTimeKind.Local).AddTicks(161),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 87, DateTimeKind.Local).AddTicks(273));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 288, DateTimeKind.Local).AddTicks(6894),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 79, DateTimeKind.Local).AddTicks(5631));

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                table: "Orders",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 172, DateTimeKind.Local).AddTicks(4071),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 380, DateTimeKind.Local).AddTicks(484));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 87, DateTimeKind.Local).AddTicks(273),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 296, DateTimeKind.Local).AddTicks(161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 28, 1, 53, 26, 79, DateTimeKind.Local).AddTicks(5631),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 28, 13, 29, 55, 288, DateTimeKind.Local).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5cb184b2-3b04-487c-8de6-65b46d36cf33");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "457a67d8-6022-46db-a583-341bd18dd540");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "0849a020-50d5-48ad-aa08-74c2b742eeea");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "7dcca001-9169-4e7d-ad21-ea16b48cd2a2", "AQAAAAEAACcQAAAAEN6vYX56rLvSATqIv3tZ2FpNKik+RGd9pY2dWaiz23auuUS6MWxpraNU3CIOnpfsfA==", new DateTime(2021, 12, 28, 1, 53, 26, 336, DateTimeKind.Local).AddTicks(6814) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

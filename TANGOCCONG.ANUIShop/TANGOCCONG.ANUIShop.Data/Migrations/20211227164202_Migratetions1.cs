using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    public partial class Migratetions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BankTranNo",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TmnCode",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TransactionCode",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TransactionID",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 329, DateTimeKind.Local).AddTicks(425),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 867, DateTimeKind.Local).AddTicks(8793));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 298, DateTimeKind.Local).AddTicks(4615),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 836, DateTimeKind.Local).AddTicks(7889));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 295, DateTimeKind.Local).AddTicks(587),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 833, DateTimeKind.Local).AddTicks(4278));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "BankTranNo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TmnCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionCode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionID",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 867, DateTimeKind.Local).AddTicks(8793),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 329, DateTimeKind.Local).AddTicks(425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "Images",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 836, DateTimeKind.Local).AddTicks(7889),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 298, DateTimeKind.Local).AddTicks(4615));

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeCreated",
                table: "AppUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 19, 22, 9, 53, 833, DateTimeKind.Local).AddTicks(4278),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 27, 23, 42, 1, 295, DateTimeKind.Local).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "612527d7-94a7-4fe9-b424-86eb0bb039e8");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "75234037-90e6-45fb-8b80-0ab1d4e2af6b");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "93c628da-f49a-4f8d-aca7-08a330e27d60");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "TimeCreated" },
                values: new object[] { "d4cac47f-2e03-4395-ac53-0b0311cdcd85", "AQAAAAEAACcQAAAAEOrsM7ARGy1KdKFubqK7tgMGexTHyt123zdjJWqYsDxq6feZwmoDN6jYdWorahXLDw==", new DateTime(2021, 12, 19, 22, 9, 53, 912, DateTimeKind.Local).AddTicks(6028) });
        }
    }
}

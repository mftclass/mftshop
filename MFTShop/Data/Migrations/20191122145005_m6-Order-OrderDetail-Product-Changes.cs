using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MFTShop.Data.Migrations
{
    public partial class m6OrderOrderDetailProductChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UnitPriceBuy",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "OrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPriceBuy",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

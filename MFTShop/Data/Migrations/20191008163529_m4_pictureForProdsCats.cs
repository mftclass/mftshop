using Microsoft.EntityFrameworkCore.Migrations;

namespace MFTShop.Data.Migrations
{
    public partial class m4_pictureForProdsCats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureAddress",
                table: "Products",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureAddress",
                table: "Categories",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureAddress",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureAddress",
                table: "Categories");
        }
    }
}

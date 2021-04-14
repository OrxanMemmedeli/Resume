using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class DatabaseRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyBlog",
                table: "BlogCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "BlogCategories",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "BlogCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MyBlog",
                table: "BlogCategories",
                type: "nvarchar(max)",
                nullable: true);

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class ContactNewColumnRespons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Respons",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Respons",
                table: "Contacts");
        }
    }
}

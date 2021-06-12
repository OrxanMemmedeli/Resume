using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class ContactNewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsMessage",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsMessage",
                table: "Contacts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class EducationAndExperienceTableAddNewBOOLTypeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Present",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowDay",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowMonth",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowYear",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Present",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowDay",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowMonth",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowYear",
                table: "Educations",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Present",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ShowDay",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ShowMonth",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "ShowYear",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Present",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ShowDay",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ShowMonth",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "ShowYear",
                table: "Educations");
        }
    }
}

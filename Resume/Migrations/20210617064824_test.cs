using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionNamesID",
                table: "ControllerActionUsers");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActionUsers_ActionNamesID",
                table: "ControllerActionUsers");

            migrationBuilder.DropColumn(
                name: "ActionNamesID",
                table: "ControllerActionUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionNamesID",
                table: "ControllerActionUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionUsers_ActionNamesID",
                table: "ControllerActionUsers",
                column: "ActionNamesID");

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionNamesID",
                table: "ControllerActionUsers",
                column: "ActionNamesID",
                principalTable: "ActionNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class NewMTMrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionID",
                table: "ControllerActionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_ControllerNames_ControllerID",
                table: "ControllerActionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_Users_UserID",
                table: "ControllerActionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControllerActionUsers",
                table: "ControllerActionUsers");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActionUsers_ControllerID",
                table: "ControllerActionUsers");

            migrationBuilder.DropColumn(
                name: "ActionID",
                table: "ControllerActionUsers");

            migrationBuilder.RenameTable(
                name: "ControllerActionUsers",
                newName: "ControllerActionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ControllerActionUsers_UserID",
                table: "ControllerActionUsers",
                newName: "IX_ControllerActionUsers_UserID");

            migrationBuilder.AddColumn<int>(
                name: "ControllerNamesID",
                table: "ActionNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActionNamesID",
                table: "ControllerActionUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControllerActionUsers",
                table: "ControllerActionUsers",
                columns: new[] { "ControllerID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_ActionNames_ControllerNamesID",
                table: "ActionNames",
                column: "ControllerNamesID");

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionUsers_ActionNamesID",
                table: "ControllerActionUsers",
                column: "ActionNamesID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionNames_ControllerNames_ControllerNamesID",
                table: "ActionNames",
                column: "ControllerNamesID",
                principalTable: "ControllerNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionNamesID",
                table: "ControllerActionUsers",
                column: "ActionNamesID",
                principalTable: "ActionNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_ControllerNames_ControllerID",
                table: "ControllerActionUsers",
                column: "ControllerID",
                principalTable: "ControllerNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_Users_UserID",
                table: "ControllerActionUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionNames_ControllerNames_ControllerNamesID",
                table: "ActionNames");

            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionNamesID",
                table: "ControllerActionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_ControllerNames_ControllerID",
                table: "ControllerActionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActionUsers_Users_UserID",
                table: "ControllerActionUsers");

            migrationBuilder.DropIndex(
                name: "IX_ActionNames_ControllerNamesID",
                table: "ActionNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControllerActionUsers",
                table: "ControllerActionUsers");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActionUsers_ActionNamesID",
                table: "ControllerActionUsers");

            migrationBuilder.DropColumn(
                name: "ControllerNamesID",
                table: "ActionNames");

            migrationBuilder.DropColumn(
                name: "ActionNamesID",
                table: "ControllerActionUsers");

            migrationBuilder.RenameTable(
                name: "ControllerActionUsers",
                newName: "ControllerActionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ControllerActionUsers_UserID",
                table: "ControllerActionUsers",
                newName: "IX_ControllerActionUsers_UserID");

            migrationBuilder.AddColumn<int>(
                name: "ActionID",
                table: "ControllerActionUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControllerActionUsers",
                table: "ControllerActionUsers",
                columns: new[] { "ActionID", "ControllerID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionUsers_ControllerID",
                table: "ControllerActionUsers",
                column: "ControllerID");

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_ActionNames_ActionID",
                table: "ControllerActionUsers",
                column: "ActionID",
                principalTable: "ActionNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_ControllerNames_ControllerID",
                table: "ControllerActionUsers",
                column: "ControllerID",
                principalTable: "ControllerNames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActionUsers_Users_UserID",
                table: "ControllerActionUsers",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

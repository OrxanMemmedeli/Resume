using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class changeMTMTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ControllerActions_Users_UserID",
                table: "ControllerActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControllerActions",
                table: "ControllerActions");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActions_ControllerID",
                table: "ControllerActions");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActions_UserID",
                table: "ControllerActions");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ControllerActions",
                newName: "ID");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ControllerActions_ID",
                table: "ControllerActions",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControllerActions",
                table: "ControllerActions",
                columns: new[] { "ControllerID", "ActionID" });

            migrationBuilder.CreateTable(
                name: "ControllerActionUsers",
                columns: table => new
                {
                    ControllerActionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerActionUsers", x => new { x.ControllerActionID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ControllerActionUsers_ControllerActions_ControllerActionID",
                        column: x => x.ControllerActionID,
                        principalTable: "ControllerActions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControllerActionUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActions_ActionID",
                table: "ControllerActions",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionUsers_UserID",
                table: "ControllerActionUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerActionUsers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ControllerActions_ID",
                table: "ControllerActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ControllerActions",
                table: "ControllerActions");

            migrationBuilder.DropIndex(
                name: "IX_ControllerActions_ActionID",
                table: "ControllerActions");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ControllerActions",
                newName: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ControllerActions",
                table: "ControllerActions",
                columns: new[] { "ActionID", "ControllerID", "UserID" });

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActions_ControllerID",
                table: "ControllerActions",
                column: "ControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActions_UserID",
                table: "ControllerActions",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ControllerActions_Users_UserID",
                table: "ControllerActions",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

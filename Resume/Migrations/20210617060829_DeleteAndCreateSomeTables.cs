using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class DeleteAndCreateSomeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleControls");

            migrationBuilder.DropTable(
                name: "RoleControls");

            migrationBuilder.CreateTable(
                name: "ActionUsers",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionUsers", x => new { x.ActionID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ActionUsers_ActionNames_ActionID",
                        column: x => x.ActionID,
                        principalTable: "ActionNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionUsers_UserID",
                table: "ActionUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionUsers");

            migrationBuilder.CreateTable(
                name: "RoleControls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleControls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleControls",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleControls", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_UserRoleControls_RoleControls_RoleID",
                        column: x => x.RoleID,
                        principalTable: "RoleControls",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleControls_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleControls_RoleID",
                table: "UserRoleControls",
                column: "RoleID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class ThreeTableMTMRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionNames",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ControllerNames",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerNames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ControllerActionUsers",
                columns: table => new
                {
                    ControllerID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControllerActionUsers", x => new { x.ActionID, x.ControllerID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ControllerActionUsers_ActionNames_ActionID",
                        column: x => x.ActionID,
                        principalTable: "ActionNames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControllerActionUsers_ControllerNames_ControllerID",
                        column: x => x.ControllerID,
                        principalTable: "ControllerNames",
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
                name: "IX_ControllerActionUsers_ControllerID",
                table: "ControllerActionUsers",
                column: "ControllerID");

            migrationBuilder.CreateIndex(
                name: "IX_ControllerActionUsers_UserID",
                table: "ControllerActionUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControllerActionUsers");

            migrationBuilder.DropTable(
                name: "ActionNames");

            migrationBuilder.DropTable(
                name: "ControllerNames");
        }
    }
}

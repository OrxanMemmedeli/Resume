using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class NewTablesForRoleAndTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoURL",
                table: "Portfolios",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoURL",
                table: "Portfolios",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HideTables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HideTables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RoleControls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ControllerName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(100)", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HideTables");

            migrationBuilder.DropTable(
                name: "UserRoleControls");

            migrationBuilder.DropTable(
                name: "RoleControls");

            migrationBuilder.DropColumn(
                name: "FotoURL",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "VideoURL",
                table: "Portfolios");
        }
    }
}

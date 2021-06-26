using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resume.Migrations
{
    public partial class deleteClientIp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientIPs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientIPs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AtempTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientIPAdress = table.Column<string>(type: "varchar(15)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIPs", x => x.ID);
                });
        }
    }
}

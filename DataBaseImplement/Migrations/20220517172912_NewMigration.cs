using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseImplement.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    GSMId = table.Column<int>(type: "int", nullable: false),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceForL = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => new { x.GSMId, x.PriceDate });
                    table.ForeignKey(
                        name: "FK_Prices_GSMs_GSMId",
                        column: x => x.GSMId,
                        principalTable: "GSMs",
                        principalColumn: "GSMId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}

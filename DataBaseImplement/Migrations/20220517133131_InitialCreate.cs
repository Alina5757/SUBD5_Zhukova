using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseImplement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GSMs",
                columns: table => new
                {
                    GSMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GSMName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSMs", x => x.GSMId);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                });

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

            migrationBuilder.CreateTable(
                name: "Sells",
                columns: table => new
                {
                    SellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    GSMId = table.Column<int>(type: "int", nullable: false),
                    SellValue = table.Column<double>(type: "float", nullable: false),
                    SellDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sells", x => x.SellId);
                    table.ForeignKey(
                        name: "FK_Sells_GSMs_GSMId",
                        column: x => x.GSMId,
                        principalTable: "GSMs",
                        principalColumn: "GSMId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sells_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Smenas",
                columns: table => new
                {
                    SmenaDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SmenaNumber = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smenas", x => new { x.SmenaDate, x.SmenaNumber, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_Smenas_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerGSMs",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    GSMId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerGSMs", x => new { x.GSMId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_WorkerGSMs_GSMs_GSMId",
                        column: x => x.GSMId,
                        principalTable: "GSMs",
                        principalColumn: "GSMId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerGSMs_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sells_GSMId",
                table: "Sells",
                column: "GSMId");

            migrationBuilder.CreateIndex(
                name: "IX_Sells_WorkerId",
                table: "Sells",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Smenas_WorkerId",
                table: "Smenas",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerGSMs_WorkerId",
                table: "WorkerGSMs",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Sells");

            migrationBuilder.DropTable(
                name: "Smenas");

            migrationBuilder.DropTable(
                name: "WorkerGSMs");

            migrationBuilder.DropTable(
                name: "GSMs");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
